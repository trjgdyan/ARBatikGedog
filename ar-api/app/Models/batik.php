<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class batik extends Model
{
    /** @use HasFactory<\Database\Factories\BatikFactory> */
    use HasFactory;

    protected $fillable = [
        'name',
        'deskripsi',
        'harga',
        'size',
        'gambar',
        'model3d',
    ];
    public function images()
    {
        return $this->hasMany(BatikImage::class);
    }
}
