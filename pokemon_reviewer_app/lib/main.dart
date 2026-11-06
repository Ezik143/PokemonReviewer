import 'package:flutter/material.dart';
import 'package:pokemon_reviewer_app/widgets/PokemonScreen.dart'; // Adjust import

void main() {
  runApp(const MainApp());
}

class MainApp extends StatelessWidget {
  const MainApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      theme: ThemeData(
        brightness: Brightness.dark,
        colorScheme: ColorScheme.fromSeed(
          seedColor: Colors.teal,
          brightness: Brightness.dark,
        ),
      ),
      title: 'Pokemon App',
      home: PokemonScreen(),
    );
  }
}
