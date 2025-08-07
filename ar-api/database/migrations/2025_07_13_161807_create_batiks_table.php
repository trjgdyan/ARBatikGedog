<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     */
    public function up(): void
    {
        Schema::create('batiks', function (Blueprint $table) {
            $table->id();
            $table->timestamps();
            $table->string('name');
            $table->text('deskripsi');
            $table->double('harga');
            $table->string('size');
            // $table->string('gambar')->nullable();
            $table->string('model3d')->nullable();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('batiks');
    }
};
