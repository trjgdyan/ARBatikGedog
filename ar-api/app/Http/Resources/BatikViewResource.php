<?php

namespace App\Http\Resources;

use Illuminate\Database\Eloquent\Casts\Json;
use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\ResourceCollection;
use Illuminate\Http\Resources\Json\JsonResource;

class BatikViewResource extends JsonResource
{
    /**
     * Transform the resource collection into an array.
     *
     * @return array<int|string, mixed>
     */
    public function toArray(Request $request): array
    {
        // return [
        //     'id' => $this->id,
        //     'nama' => $this->name,
        //     'deskripsi' => $this->deskripsi,
        //     'harga' => $this->harga,
        //     'size' => $this->size,
        //     // gambar adalah link public dari gambar batik
        //     'gambar' => url('/gambar/' . $this->gambar),
        //     'model3d' => $this->model3d,
        // ];
        return [
            'id' => $this['id'],
            'nama' => $this['nama'],
            'deskripsi' => $this['deskripsi'], 
            'harga' => $this['harga'],
            'size' => $this['size'],
            'gambar' => $this['gambar'],
            'model3d' => $this['model3d'],
        ];
    }
}
