<?php

namespace App\Http\Controllers;

use App\Models\batik;
use App\Http\Requests\StorebatikRequest;
use App\Http\Requests\UpdatebatikRequest;
use App\Http\Resources\BatikView;
use App\Http\Resources\BatikViewResource;

class BatikController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        $batiks = Batik::with('images')
            ->get()
            ->groupBy('name')
            ->map(function ($items) {
                $first = $items->first();
                return [
                    'id' => $first->id,
                    'nama' => $first->name,
                    'deskripsi' => $first->deskripsi,
                    'harga' => $first->harga,
                    'size' => $items->map(function ($item) {
                        return [
                            'id' => $item->id,
                            'size' => $item->size,
                        ];
                    })->unique('size')->values(),
                    'gambar' => $first->images->map(function ($img) {
                        return asset('gambar/' . $img->image_path);
                    }),
                    'model3d' => $first->model3d,
                ];
            })
            ->values();

        return BatikViewResource::collection($batiks);
    }   

    /**
     * Show the form for creating a new resource.
     */
    public function create()
    {
        //
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StorebatikRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(batik $batik)
    {
        return new BatikViewResource($batik);
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(batik $batik)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdatebatikRequest $request, batik $batik)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(batik $batik)
    {
        //
    }
}
