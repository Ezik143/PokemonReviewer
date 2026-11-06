import 'package:flutter/material.dart';
import 'package:pokemon_reviewer_app/models/pokemon.dart';
import 'package:pokemon_reviewer_app/services/api_service.dart';

class PokemonScreen extends StatefulWidget {
  const PokemonScreen({super.key});

  @override
  State<PokemonScreen> createState() => _PokemonScreenState();
}

class _PokemonScreenState extends State<PokemonScreen> {
  late Future<List<Pokemon>> futurePokemons;

  final ApiService apiService = ApiService();

  @override
  void initState() {
    super.initState();
    futurePokemons = apiService.fetchPokemon();
  }

  Future<void> _addPokemon() async {
    final nameController = TextEditingController();
    DateTime? selectedBirthdate;

    final formKey = GlobalKey<FormState>();

    final pokemon = await showDialog<Pokemon>(
      context: context,
      builder: (context) {
        return StatefulBuilder(
          builder: (context, setDialogState) {
            return AlertDialog(
              title: const Text('Add Pokémon'),
              content: Form(
                key: formKey,
                child: Column(
                  mainAxisSize: MainAxisSize.min,
                  children: [
                    TextFormField(
                      controller: nameController,
                      decoration: const InputDecoration(
                        labelText: 'Name',
                        border: OutlineInputBorder(),
                      ),
                      validator: (value) {
                        if (value == null || value.trim().isEmpty) {
                          return 'Enter a Pokémon name';
                        }
                        return null;
                      },
                    ),
                    const SizedBox(height: 16),
                    TextFormField(
                      readOnly: true,
                      decoration: InputDecoration(
                        labelText: 'Birthdate',
                        border: const OutlineInputBorder(),
                        hintText: selectedBirthdate == null
                            ? 'Select a date'
                            : '${selectedBirthdate!.year}-'
                                  '${selectedBirthdate!.month.toString().padLeft(2, '0')}-'
                                  '${selectedBirthdate!.day.toString().padLeft(2, '0')}',
                        suffixIcon: const Icon(Icons.calendar_today),
                      ),
                      onTap: () async {
                        final date = await showDatePicker(
                          context: context,
                          firstDate: DateTime(1900),
                          lastDate: DateTime.now(),
                          initialDate: DateTime.now(),
                        );

                        if (date != null) {
                          setDialogState(() {
                            selectedBirthdate = date;
                          });
                        }
                      },
                      validator: (_) {
                        if (selectedBirthdate == null) {
                          return 'Select a birthdate';
                        }
                        return null;
                      },
                    ),
                  ],
                ),
              ),
              actions: [
                TextButton(
                  onPressed: () => Navigator.pop(context),
                  child: const Text('Cancel'),
                ),
                ElevatedButton(
                  onPressed: () {
                    if (!formKey.currentState!.validate()) {
                      return;
                    }

                    Navigator.pop(
                      context,
                      Pokemon(
                        id: 0,
                        name: nameController.text.trim(),
                        birthdate: selectedBirthdate!,
                      ),
                    );
                  },
                  child: const Text('Save'),
                ),
              ],
            );
          },
        );
      },
    );

    nameController.dispose();

    if (pokemon == null) {
      return;
    }

    try {
      await apiService.createPokemon(pokemon);

      if (!mounted) return;

      setState(() {
        futurePokemons = apiService.fetchPokemon();
      });

      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Pokémon added successfully')),
      );
    } catch (error) {
      if (!mounted) return;

      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text('Failed to add Pokémon: $error')));
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Pokémon List'),
        centerTitle: true,
        actions: [
          IconButton(onPressed: _addPokemon, icon: const Icon(Icons.add)),
        ],
      ),
      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: FutureBuilder<List<Pokemon>>(
          future: futurePokemons,
          builder: (context, snapshot) {
            if (snapshot.hasData) {
              final pokemons = snapshot.data!;

              return ListView.builder(
                itemCount: pokemons.length,
                itemBuilder: (context, index) {
                  final pokemon = pokemons[index];

                  return Card(
                    margin: const EdgeInsets.symmetric(
                      vertical: 6,
                      horizontal: 4,
                    ),
                    child: Padding(
                      padding: const EdgeInsets.all(12),
                      child: Row(
                        children: [
                          CircleAvatar(child: Text('${pokemon.id}')),
                          const SizedBox(width: 12),
                          Expanded(
                            child: Text(
                              pokemon.name,
                              style: const TextStyle(fontSize: 18),
                            ),
                          ),
                        ],
                      ),
                    ),
                  );
                },
              );
            }

            if (snapshot.hasError) {
              return Center(child: Text('Error: ${snapshot.error}'));
            }

            return const Center(child: CircularProgressIndicator());
          },
        ),
      ),
    );
  }
}
