# Pokemon Reviewer Flutter app

This Flutter app reads Pokemon from the ASP.NET Core API in the sibling
`PokemonReviewApp` folder.

## Run locally

Start the API first:

```powershell
cd ..\PokemonReviewApp
dotnet run --launch-profile http
```

Then run Flutter in another terminal:

```powershell
flutter pub get
flutter run -d windows
```

For Flutter Web, use `flutter run -d chrome`. The API enables CORS only in
Development so the browser can call it.

The app calls:

```text
GET http://localhost:5109/api/Pokemon/pokemons
```

Android emulators use `10.0.2.2` automatically because their `localhost` is
the emulator itself. A physical phone needs the computer's LAN address:

```powershell
flutter run --dart-define=API_BASE_URL=http://192.168.1.10:5109/api
```

## Request flow

`PokemonListScreen` starts a `Future` in `initState`. `ApiService` sends the
HTTP request, checks the status code, decodes the JSON, and converts each
object with `Pokemon.fromJson`. `FutureBuilder` then renders loading, error,
empty, or list states.
