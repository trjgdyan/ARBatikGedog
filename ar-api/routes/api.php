<?php

use App\Http\Controllers\BatikController;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;

Route::get('/user', function (Request $request) {
    return $request->user();
})->middleware('auth:sanctum');

Route::get('/batiks', [BatikController::class, 'index'])->name('batiks.index');
Route::get('/batiks/{batik}', [BatikController::class, 'show'])->name('batiks.show');

