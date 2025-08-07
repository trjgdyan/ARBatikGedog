<?php

use Illuminate\Support\Facades\Route;

Route::get('/', function () {
    return view('welcome');
});
Route::get('/gambar/{filename}', function ($filename) {
    $path = public_path('gambar/' . $filename);
    if (!file_exists($path)) {
        abort(404);
    }
    return response()->file($path)->header('Access-Control-Allow-Origin', '*');
});
