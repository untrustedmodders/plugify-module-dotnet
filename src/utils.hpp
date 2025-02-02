#pragma once

#include <cstring>
#include <plugify/method.hpp>

#if NETLM_PLATFORM_WINDOWS
#define NETLM_NSTR(str) L##str
#define NETLM_UTF8(str) Utils::ConvertWideToUtf8(str)
#define NETLM_PSTR(str) Utils::ConvertUtf8ToWide(str)
#else
#define NETLM_NSTR(str) str
#define NETLM_UTF8(str) str
#define NETLM_PSTR(str) str
#endif

namespace netlm {
	class Utils {
	public:
		Utils() = delete;

#if NETLM_PLATFORM_WINDOWS
		/// Converts the specified UTF-8 string to a wide string.
		static std::wstring ConvertUtf8ToWide(std::string_view str);
		static bool ConvertUtf8ToWide(std::wstring& dest, std::string_view str);

		/// Converts the specified wide string to a UTF-8 string.
		static std::string ConvertWideToUtf8(std::wstring_view str);
		static bool ConvertWideToUtf8(std::string& dest, std::wstring_view str);
#endif

		static std::vector<std::string_view> Split(std::string_view strv, std::string_view delims = " ");
	};
}
