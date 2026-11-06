import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:pokemon_reviewer_app/core/api_config.dart';
import 'package:pokemon_reviewer_app/models/pokemon.dart';

class ApiService {

  Future<List<Pokemon>> fetchPokemon() async {
    final response = await http.get(
      Uri.parse('${ApiConfig.baseUrl}/Pokemon/pokemons'),
    );

    if (response.statusCode == 200) {
      final data = jsonDecode(response.body) as List<dynamic>;
      return data
          .map((json) => Pokemon.fromJson(json as Map<String, dynamic>))
          .toList();
    }

    throw Exception(
      'Failed to load Pokemon. Status code: ${response.statusCode}',
    );
  }

  Future<Pokemon> createPokemon(Pokemon pokemon) async {
    final response = await http.post(
      Uri.parse('${ApiConfig.baseUrl}/Pokemon/pokemons'),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(pokemon.toJson()),
    );

    if (response.statusCode == 200 || response.statusCode == 201) {
      final data = jsonDecode(response.body) as Map<String, dynamic>;

      return Pokemon.fromJson(data);
    }

    throw Exception(
      'Failed to create Pokemon. Status code: ${response.statusCode}',
    );
  }
}
