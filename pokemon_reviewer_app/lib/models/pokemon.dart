class Pokemon {
  final int? id;
  final String name;
  final DateTime birthdate;

  Pokemon({this.id, required this.name, required this.birthdate});

  factory Pokemon.fromJson(Map<String, dynamic> json) => Pokemon(
    id: (json['id'] as num).toInt(),
    name: json['name'] as String,
    birthdate: DateTime.parse(json['birthDate'] as String),
  );
}
