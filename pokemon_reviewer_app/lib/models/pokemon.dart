class Pokemon {
  final int id;
  final String name;
  final DateTime birthdate;

  Pokemon({required this.id, required this.name, required this.birthdate});

  Map<String, dynamic> toJson() => {
    'name': name,
    'birthDate': birthdate.toIso8601String(),
  };

  factory Pokemon.fromJson(Map<String, dynamic> json) => Pokemon(
    id: (json['id'] as num).toInt(),
    name: json['name'] as String,
    birthdate: DateTime.parse(json['birthDate'] as String),
  );
}
