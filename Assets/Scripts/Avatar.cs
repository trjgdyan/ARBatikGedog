using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Avatar : MonoBehaviour
{
    public Camera previewCamera; // OPTIONAL
    public Animator animator;
    public LayerMask ground;
    public bool footTracking = true;
    public float footGroundOffset = .1f;
    [Header("Calibration")]
    public bool useCalibrationData = false;
    public PersistentCalibrationData calibrationData;

    public bool Calibrated { get; private set; }


    private PipeServer server;


    private Quaternion initialRotation;
    private Vector3 initialPosition;
    private Quaternion targetRot;

    // private Vector3 initialAvatarPosition;
    // private Vector3 calibrationUserPosition;
    // private Vector3 initialAvatarScale;
    // private float calibrationUserShoulderWidth;
    // public vector3 d;
    // public Queternion deltaRotTracked;

    // variabel kalibrasi untuk posisi dan skala
    private Vector3 initialAvatarPosition; //posisi avatar saat kalibrasi
    private Vector3 initialAvatarScale; // skala avatar saat kalibrasi

    private Vector3 calibrationUserPosition; // posisi user dari mediapepe saat kalibrasi
    private float calibrationUserShoulderWidth; // lebar bahu user dari mediapepe saat kalibrasi



    private Dictionary<HumanBodyBones, CalibrationData> parentCalibrationData = new Dictionary<HumanBodyBones, CalibrationData>();
    private CalibrationData spineUpDown, hipsTwist, chest, head;

    private void Start()
    {
        initialRotation = transform.rotation;
        initialPosition = transform.position;
        initialAvatarScale = transform.localScale;

        if (calibrationData && useCalibrationData)
        {
            CalibrateFromPersistent();
        }

        server = FindObjectOfType<PipeServer>();
        if (server == null)
        {
            Debug.LogError("You must have a PipeServer in the scene!");
        }

    }

    public void CalibrateFromPersistent()
    {
        parentCalibrationData.Clear();

        if (calibrationData)
        {
            foreach (PersistentCalibrationData.CalibrationEntry d in calibrationData.parentCalibrationData)
            {
                parentCalibrationData.Add(d.bone, d.data.ReconstructReferences());
            }
            spineUpDown = calibrationData.spineUpDown.ReconstructReferences();
            hipsTwist = calibrationData.hipsTwist.ReconstructReferences();
            chest = calibrationData.chest.ReconstructReferences();
            head = calibrationData.head.ReconstructReferences();
        }

        animator.enabled = false; // disable animator to stop interference.
        Calibrated = true;
    }
    public void Calibrate()
    {
        // Here we store the values of variables required to do the correct rotations at runtime.
        print("Calibrating on " + gameObject.name);

        parentCalibrationData.Clear();

        // Manually setting calibration data for the spine chain as we want really specific control over that.
        spineUpDown = new CalibrationData(animator.transform, animator.GetBoneTransform(HumanBodyBones.Spine), animator.GetBoneTransform(HumanBodyBones.Neck),
            server.GetVirtualHip(), server.GetVirtualNeck());
        hipsTwist = new CalibrationData(animator.transform, animator.GetBoneTransform(HumanBodyBones.Hips), animator.GetBoneTransform(HumanBodyBones.Hips),
            server.GetLandmark(Landmark.RIGHT_HIP), server.GetLandmark(Landmark.LEFT_HIP));
        chest = new CalibrationData(animator.transform, animator.GetBoneTransform(HumanBodyBones.Chest), animator.GetBoneTransform(HumanBodyBones.Chest),
            server.GetLandmark(Landmark.RIGHT_HIP), server.GetLandmark(Landmark.LEFT_HIP));
        head = new CalibrationData(animator.transform, animator.GetBoneTransform(HumanBodyBones.Neck), animator.GetBoneTransform(HumanBodyBones.Head),
            server.GetVirtualNeck(), server.GetLandmark(Landmark.NOSE));


        // Adding calibration data automatically for the rest of the bones.
        AddCalibration(HumanBodyBones.RightUpperArm, HumanBodyBones.RightLowerArm,
            server.GetLandmark(Landmark.RIGHT_SHOULDER), server.GetLandmark(Landmark.RIGHT_ELBOW));
        AddCalibration(HumanBodyBones.RightLowerArm, HumanBodyBones.RightHand,
            server.GetLandmark(Landmark.RIGHT_ELBOW), server.GetLandmark(Landmark.RIGHT_WRIST));

        AddCalibration(HumanBodyBones.RightUpperLeg, HumanBodyBones.RightLowerLeg,
            server.GetLandmark(Landmark.RIGHT_HIP), server.GetLandmark(Landmark.RIGHT_KNEE));
        AddCalibration(HumanBodyBones.RightLowerLeg, HumanBodyBones.RightFoot,
            server.GetLandmark(Landmark.RIGHT_KNEE), server.GetLandmark(Landmark.RIGHT_ANKLE));

        AddCalibration(HumanBodyBones.LeftUpperArm, HumanBodyBones.LeftLowerArm,
            server.GetLandmark(Landmark.LEFT_SHOULDER), server.GetLandmark(Landmark.LEFT_ELBOW));
        AddCalibration(HumanBodyBones.LeftLowerArm, HumanBodyBones.LeftHand,
            server.GetLandmark(Landmark.LEFT_ELBOW), server.GetLandmark(Landmark.LEFT_WRIST));

        AddCalibration(HumanBodyBones.LeftUpperLeg, HumanBodyBones.LeftLowerLeg,
            server.GetLandmark(Landmark.LEFT_HIP), server.GetLandmark(Landmark.LEFT_KNEE));
        AddCalibration(HumanBodyBones.LeftLowerLeg, HumanBodyBones.LeftFoot,
            server.GetLandmark(Landmark.LEFT_KNEE), server.GetLandmark(Landmark.LEFT_ANKLE));

        if (footTracking)
        {
            AddCalibration(HumanBodyBones.LeftFoot, HumanBodyBones.LeftToes,
                server.GetLandmark(Landmark.LEFT_ANKLE), server.GetLandmark(Landmark.LEFT_FOOT_INDEX));
            AddCalibration(HumanBodyBones.RightFoot, HumanBodyBones.RightToes,
                server.GetLandmark(Landmark.RIGHT_ANKLE), server.GetLandmark(Landmark.RIGHT_FOOT_INDEX));
        }


        animator.enabled = false; // disable animator to stop interference.
        Calibrated = true;

        Debug.Log("Calibrating position and scale references...");
        if (server != null)
        {
            // kode baru start

            initialAvatarPosition = transform.position;
            initialAvatarScale = transform.localScale;

            calibrationUserPosition = server.receivedPosition;

            Transform leftShoulder = server.GetLandmark(Landmark.LEFT_SHOULDER);
            Transform rightShoulder = server.GetLandmark(Landmark.RIGHT_SHOULDER);

            if (leftShoulder != null && rightShoulder != null)
            {
                // Penting: Gunakan posisi landmark yang sudah di-transformasi ke Unity space oleh PipeServer
                // (yaitu, setelah dikalikan multiplier dan koreksi koordinat dasar)
                calibrationUserShoulderWidth = Vector3.Distance(leftShoulder.position, rightShoulder.position);
                // Fallback jika jarak terlalu kecil (mungkin error pelacakan atau user terlalu jauh)
                if (calibrationUserShoulderWidth < 0.01f) // Toleransi kecil untuk menghindari nol
                {
                    // Gunakan nilai default yang masuk akal jika deteksi gagal
                    calibrationUserShoulderWidth = 0.4f; // Contoh: 40cm antar bahu
                    Debug.LogWarning("User shoulder width was too small during calibration, using a default value: " + calibrationUserShoulderWidth);
                }
            }
            else
            {
                Debug.LogError("Shoulder landmarks not found during calibration! Cannot calibrate scale accurately.");
                // Tetapkan nilai default jika landmark tidak ditemukan
                calibrationUserShoulderWidth = 0.4f;
            }

            Debug.Log($"Calibration Complete: User Pos={calibrationUserPosition}, User Shoulder Width={calibrationUserShoulderWidth}");
            Debug.Log($"Avatar Initial Pos={initialAvatarPosition}, Avatar Initial Scale={initialAvatarScale}");
        }
        else
        {
            Debug.LogError("PipeServer not found! Cannot perform position and scale calibration.");




            // // Simpan posisi user dari server saat kalibrasi
            // this.calibrationUserPosition = server.receivedPosition;

            // // Hitung dan simpan jarak antar bahu sebagai referensi ukuran
            // Transform leftShoulder = server.GetLandmark(Landmark.LEFT_SHOULDER);
            // Transform rightShoulder = server.GetLandmark(Landmark.RIGHT_SHOULDER);
            // if (leftShoulder != null && rightShoulder != null)
            // {
            //     this.calibrationUserShoulderWidth = Vector3.Distance(leftShoulder.position, rightShoulder.position);
            //     // Fallback jika jarak terlalu kecil (mungkin error pelacakan)
            //     if (this.calibrationUserShoulderWidth < 0.01f)
            //     {
            //         this.calibrationUserShoulderWidth = 0.4f; // Nilai default yang masuk akal (40cm)
            //         Debug.LogWarning("User shoulder width was too small, using a default value.");
            //     }
            // }
            // else
            // {
            //     Debug.LogError("Shoulder landmarks not found! Cannot calibrate scale.");
            // }
        }
    }

    private bool IsValidVector(Vector3 v)
    {
        // Pengecekan dengan toleransi epsilon untuk memastikan data tidak nol atau sangat kecil
        return v.sqrMagnitude > 0.0001f; // Menggunakan sqrMagnitude lebih efisien daripada Abs untuk setiap komponen
    }

    public void StoreCalibration()
    {
        if (!calibrationData)
        {
            Debug.LogError("Optional calibration data must be assigned to store into.");
            return;
        }

        List<PersistentCalibrationData.CalibrationEntry> calibrations = new List<PersistentCalibrationData.CalibrationEntry>();
        foreach (KeyValuePair<HumanBodyBones, CalibrationData> k in parentCalibrationData)
        {
            calibrations.Add(new PersistentCalibrationData.CalibrationEntry() { bone = k.Key, data = k.Value });
        }
        calibrationData.parentCalibrationData = calibrations.ToArray();

        calibrationData.spineUpDown = spineUpDown;
        calibrationData.hipsTwist = hipsTwist;
        calibrationData.chest = chest;
        calibrationData.head = head;

        calibrationData.Dirty();

        print("Completed storing calibration data " + calibrationData.name);
    }
    private void AddCalibration(HumanBodyBones parent, HumanBodyBones child, Transform trackParent, Transform trackChild)
    {
        parentCalibrationData.Add(parent,
            new CalibrationData(animator.transform, animator.GetBoneTransform(parent), animator.GetBoneTransform(child),
            trackParent, trackChild));
    }

    // private bool IsValidVector(Vector3 v)
    // {
    //     // Pengecekan dengan toleransi epsilon
    //     return Mathf.Abs(v.x) > 0.001f || Mathf.Abs(v.y) > 0.001f || Mathf.Abs(v.z) > 0.001f;
    // }

    private void Update()
    {
        // tidak melakukan apapun jika tidak dikalibrasi
        if (!Calibrated || parentCalibrationData.Count == 0 || server == null)
        {
            return;
        }


        foreach (var i in parentCalibrationData)
        {
            Quaternion deltaRotTracked = Quaternion.FromToRotation(i.Value.initialDir, i.Value.CurrentDirection);
            i.Value.parent.rotation = deltaRotTracked * i.Value.initialRotation;
        }

        // Hitung rotasi khusus untuk tulang belakang, dada, dan kepala
        Vector3 hd = head.CurrentDirection;
        Quaternion headr = Quaternion.FromToRotation(head.initialDir, hd);
        Quaternion twist = Quaternion.FromToRotation(hipsTwist.initialDir, Vector3.Slerp(hipsTwist.initialDir, hipsTwist.CurrentDirection, .25f));
        Quaternion updown = Quaternion.FromToRotation(spineUpDown.initialDir, Vector3.Slerp(spineUpDown.initialDir, spineUpDown.CurrentDirection, .25f));

        Quaternion h = updown * updown * updown * twist * twist;
        Quaternion s = h * twist * updown;
        Quaternion c = s * twist * twist;
        float speed = 10f;
        hipsTwist.Tick(h * hipsTwist.initialRotation, speed);
        spineUpDown.Tick(s * spineUpDown.initialRotation, speed);
        chest.Tick(c * chest.initialRotation, speed);
        head.Tick(updown * twist * headr * head.initialRotation, speed);


        // --- Langkah 2: Hitung Posisi, Skala, dan Rotasi Root Avatar ---

        if (IsValidVector(server.receivedPosition))
        {
            // // --- A. Hitung Penyesuaian Grounding ---
            // float displacement = 0;
            // RaycastHit h1;
            // if (Physics.Raycast(animator.GetBoneTransform(HumanBodyBones.LeftFoot).position, Vector3.down, out h1, 100f, ground, QueryTriggerInteraction.Ignore))
            // {
            //     displacement = (h1.point - animator.GetBoneTransform(HumanBodyBones.LeftFoot).position).y;
            // }
            // if (Physics.Raycast(animator.GetBoneTransform(HumanBodyBones.RightFoot).position, Vector3.down, out h1, 100f, ground, QueryTriggerInteraction.Ignore))
            // {
            //     float displacement2 = (h1.point - animator.GetBoneTransform(HumanBodyBones.RightFoot).position).y;
            //     if (Mathf.Abs(displacement2) < Mathf.Abs(displacement))
            //     {
            //         displacement = displacement2;
            //     }
            // }
            // Vector3 groundAdjustment = Vector3.up * (displacement + footGroundOffset);


            // // --- B. Hitung Posisi Relatif dari Kalibrasi ---
            // Vector3 userPositionDelta = server.receivedPosition - calibrationUserPosition;
            // float scaleFix = 25f;
            // Vector3 convertedPositionDelta = new Vector3(userPositionDelta.x * scaleFix, -userPositionDelta.y * scaleFix, -userPositionDelta.z * scaleFix);
            // Vector3 calibratedPosition = initialPosition + convertedPositionDelta;

            // // --- C. GABUNGKAN Keduanya dan Terapkan Posisi Final ---
            // // Kita gabungkan posisi hasil kalibrasi dengan penyesuaian grounding
            // transform.position = Vector3.Lerp(transform.position, calibratedPosition + groundAdjustment, Time.deltaTime * 10f);


            // // --- D. Terapkan Skala Relatif ---
            // Transform leftShoulder = server.GetLandmark(Landmark.LEFT_SHOULDER);
            // Transform rightShoulder = server.GetLandmark(Landmark.RIGHT_SHOULDER);
            // if (leftShoulder != null && rightShoulder != null)
            // {
            //     float currentUserShoulderWidth = Vector3.Distance(leftShoulder.position, rightShoulder.position);
            //     if (currentUserShoulderWidth > 0.01f)
            //     {
            //         float scaleMultiplier = currentUserShoulderWidth / calibrationUserShoulderWidth;
            //         transform.localScale = initialAvatarScale * scaleMultiplier;
            //     }
            // }

            // // --- E. Terapkan Rotasi Root (Hanya satu kali) ---
            // Vector3 d = Vector3.Slerp(hipsTwist.initialDir, hipsTwist.CurrentDirection, .25f);
            // d.y *= 0.5f; //mengurangi pengaruh rotasi Y(vertikal) untuk rotasi root
            // Quaternion deltaRotTracked = Quaternion.FromToRotation(hipsTwist.initialDir, d);
            // targetRot = deltaRotTracked * initialRotation;
            // transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * speed);


            // --- A. Hitung Perubahan Posisi User dari Titik Kalibrasi ---
            Vector3 userPositionDelta = server.receivedPosition - calibrationUserPosition;

            userPositionDelta = -userPositionDelta;


            // --- B. Terapkan Perubahan Posisi ke Posisi Awal Avatar ---
            // Ini akan menggerakkan avatar relatif terhadap posisi awalnya saat kalibrasi
            Vector3 targetAvatarPosition = initialAvatarPosition + userPositionDelta;

            // --- C. Hitung Penyesuaian Grounding ---
            float displacement = 0;
            RaycastHit h1;
            if (Physics.Raycast(animator.GetBoneTransform(HumanBodyBones.LeftFoot).position, Vector3.down, out h1, 100f, ground, QueryTriggerInteraction.Ignore))
            {
                displacement = (h1.point - animator.GetBoneTransform(HumanBodyBones.LeftFoot).position).y;
            }
            if (Physics.Raycast(animator.GetBoneTransform(HumanBodyBones.RightFoot).position, Vector3.down, out h1, 100f, ground, QueryTriggerInteraction.Ignore))
            {
                float displacement2 = (h1.point - animator.GetBoneTransform(HumanBodyBones.RightFoot).position).y;
                if (Mathf.Abs(displacement2) < Mathf.Abs(displacement))
                {
                    displacement = displacement2;
                }
            }
            Vector3 groundAdjustment = Vector3.up * (displacement + footGroundOffset);

            // --- D. Gabungkan Posisi Target dengan Penyesuaian Grounding ---
            transform.position = Vector3.Lerp(transform.position, targetAvatarPosition + groundAdjustment, Time.deltaTime * speed);

            // --- E. Hitung dan Terapkan Skala Relatif ---
            Transform currentLeftShoulder = server.GetLandmark(Landmark.LEFT_SHOULDER);
            Transform currentRightShoulder = server.GetLandmark(Landmark.RIGHT_SHOULDER);

            if (currentLeftShoulder != null && currentRightShoulder != null)
            {
                float currentUserShoulderWidth;
                currentUserShoulderWidth = Vector3.Distance(currentLeftShoulder.position, currentRightShoulder.position);
                if (currentUserShoulderWidth > 0.01f) // Hindari pembagian dengan nol atau nilai sangat kecil
                {
                    // Hitung rasio skala berdasarkan perbandingan lebar bahu saat ini dengan lebar bahu kalibrasi
                    float scaleMultiplier = currentUserShoulderWidth / calibrationUserShoulderWidth;
                    // Terapkan pengali ke skala awal avatar
                    transform.localScale = Vector3.Lerp(transform.localScale, initialAvatarScale * scaleMultiplier, Time.deltaTime * speed);
                }
            }

            // --- F. Terapkan Rotasi Root (dari pinggul) ---
            Vector3 d = Vector3.Slerp(hipsTwist.initialDir, hipsTwist.CurrentDirection, .25f);
            d.y *= 0.5f; //mengurangi pengaruh rotasi Y(vertikal) untuk rotasi root
            Quaternion deltaRotTracked = Quaternion.FromToRotation(hipsTwist.initialDir, d);
            targetRot = deltaRotTracked * initialRotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * speed);

            // Debug.Log($" user position delta: {userPositionDelta}, " +
            //             $"target avatar position: {targetAvatarPosition}, " +
            //             $"ground adjustment: {groundAdjustment}, " +
            //             $"current shoulder width: {currentUserShoulderWidth}, " +
            //             $"scale multiplier: {currentUserShoulderWidth / calibrationUserShoulderWidth}, " +
            //             $"new scale: {transform.localScale}");



        }
        else
        {
            Debug.LogWarning("Received position data is invalid or calibration shoulder width is zero. Skipping position/scale update.");
        }




    }


}




