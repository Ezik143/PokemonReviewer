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
}
