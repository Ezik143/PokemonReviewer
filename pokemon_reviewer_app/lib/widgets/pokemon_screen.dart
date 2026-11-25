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
    final pokemon = await showDialog<Pokemon>(
      context: context,
      builder: (_) => const _AddPokemonDialog(),
    );

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

class _AddPokemonDialog extends StatefulWidget {
  const _AddPokemonDialog();

  @override
  State<_AddPokemonDialog> createState() => _AddPokemonDialogState();
}

class _AddPokemonDialogState extends State<_AddPokemonDialog> {
  final _formKey = GlobalKey<FormState>();
  final _nameController = TextEditingController();
  DateTime? _selectedBirthdate;

  @override
  void dispose() {
    _nameController.dispose();
    super.dispose();
  }

  Future<void> _selectBirthdate() async {
    final date = await showDatePicker(
      context: context,
      firstDate: DateTime(1900),
      lastDate: DateTime.now(),
      initialDate: DateTime.now(),
    );

    if (!mounted || date == null) {
      return;
    }

    setState(() {
      _selectedBirthdate = date;
    });
  }

  void _save() {
    if (!_formKey.currentState!.validate()) {
      return;
    }

    Navigator.of(context).pop(
      Pokemon(
        id: 0,
        name: _nameController.text.trim(),
        birthdate: _selectedBirthdate!,
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    final birthdate = _selectedBirthdate;

    return AlertDialog(
      title: const Text('Add Pokémon'),
      content: Form(
        key: _formKey,
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            TextFormField(
              controller: _nameController,
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
                hintText: birthdate == null
                    ? 'Select a date'
                    : '${birthdate.year}-'
                          '${birthdate.month.toString().padLeft(2, '0')}-'
                          '${birthdate.day.toString().padLeft(2, '0')}',
                suffixIcon: const Icon(Icons.calendar_today),
              ),
              onTap: _selectBirthdate,
              validator: (_) {
                if (_selectedBirthdate == null) {
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
          onPressed: () => Navigator.of(context).pop(),
          child: const Text('Cancel'),
        ),
        ElevatedButton(onPressed: _save, child: const Text('Save')),
      ],
    );
  }
}
