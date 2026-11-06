import 'package:flutter/foundation.dart';

/// Configuration for the local ASP.NET Core API.
class ApiConfig {
  ApiConfig._();

  static const _apiPort = 5109;

  /// Override this when the API is on another machine:
  ///
  /// flutter run --dart-define=API_BASE_URL=http://192.168.1.10:5109/api
  static String get baseUrl {
    const override = String.fromEnvironment('API_BASE_URL');
    if (override.isNotEmpty) {
      return override;
    }

    // localhost means the browser/desktop machine. Android emulators use
    // 10.0.2.2 to reach the host computer running the API.
    final host = !kIsWeb && defaultTargetPlatform == TargetPlatform.android
        ? '10.0.2.2'
        : 'localhost';
    return 'http://$host:$_apiPort/api';
  }
}
