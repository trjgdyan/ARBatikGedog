<?php

namespace Database\Factories;

use Illuminate\Database\Eloquent\Factories\Factory;

/**
 * @extends \Illuminate\Database\Eloquent\Factories\Factory<\App\Models\batik>
 */
class BatikFactory extends Factory
{
    /**
     * Define the model's default state.
     *
     * @return array<string, mixed>
     */
    public function definition(): array
    {
        return [
            'name' => $this->faker->word(),
            'deskripsi' => $this->faker->sentence(),
            'harga' => $this->faker->randomFloat(2, 10000, 1000000),
            'size' => $this->faker->randomElement(['S', 'M', 'L', 'XL']),
            'gambar' => $this->faker->imageUrl(640, 480, 'fashion', true),
            'model3d' => $this->faker->imageUrl(640, 480, 'technology', true),
        ];
    }
}
