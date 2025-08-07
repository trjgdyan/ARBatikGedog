<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class BatikImage extends Model
{
    protected $fillable = ['batik_id', 'image_path'];

    public function batik()
    {
        return $this->belongsTo(Batik::class);
    }
}

