[![English](https://img.shields.io/badge/English-%F0%9F%87%AC%F0%9F%87%A7-blue?style=for-the-badge)](README.md)

# Модуль языка C# (.NET) для Plugify

Модуль языка C# (.NET) для Plugify облегчает разработку плагинов на C# для фреймворка Plugify. С его помощью вы можете бесшовно интегрировать C# плагины, позволяя ядру Plugify динамически загружать и управлять ими.

## Возможности

- **Поддержка плагинов на C# (.NET)**: Пишите плагины на C# (.NET) и легко интегрируйте их с фреймворком Plugify.
- **Автоматическая экспортируемость**: Удобный экспорт и импорт методов между плагинами и языковым модулем.
- **Инициализация и завершение работы**: Обрабатывайте запуск, инициализацию и завершение плагина с помощью событий модуля.
- **Взаимодействие между языками**: Общение с плагинами на других языках через автоматически сгенерированные интерфейсы.

**Примечание**: Все плагины на C# (.NET) размещаются в одном домене. Это обеспечивает бесшовное взаимодействие и совместную работу между C# плагинами без участия ядра Plugify.

## Начало работы

### Требования

- Среда выполнения .NET [(.NET 9.0.0)](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)  
- Установленный фреймворк Plugify

### Установка

#### Вариант 1: Установка через менеджер плагинов Plugify

Вы можете установить модуль языка C# (.NET) с помощью менеджера плагинов Plugify, выполнив следующую команду:

```
bash
plg install plugify-module-dotnet
```

#### Вариант 2: Ручная установка

1. Установите зависимости:

   a. Windows  
   > Настройка [CMake-инструментов через Visual Studio Installer](https://learn.microsoft.com/en-us/cpp/build/cmake-projects-in-visual-studio#installation)

   b. Linux:
   ```sh
   sudo apt-get install -y build-essential cmake ninja-build
   ```
   
   c. Mac:
   ```sh
   brew install cmake ninja
   ```

2. Клонируйте репозиторий:

   ```bash
   git clone https://github.com/untrustedmodders/plugify-module-dotnet.git --recursive
   cd plugify-module-dotnet
   ```

3. Сборка модуля языка C# (.NET):

   ```bash
   mkdir build && cd build
   cmake ..
   cmake --build .
   ```

### Использование

1. **Интеграция с Plugify**

   Убедитесь, что модуль языка C# (.NET) находится в той же директории, что и ваша установка Plugify.

2. **Создание плагинов на C#**

   Разрабатывайте плагины на C# с использованием API Plugify. Подробное руководство можно найти в [документации по созданию плагина на C#](https://untrustedmodders.github.io/languages/csharp/first-plugin).

3. **Сборка и установка плагинов**

   Скомпилируйте ваши C# плагины и разместите сборки (assemblies) в директории, доступной для ядра Plugify.

4. **Запуск Plugify**

   Запустите фреймворк Plugify — он автоматически загрузит ваши C# плагины.

## Пример

```csharp
using Plugify;

namespace ExamplePlugin
{
    public class SamplePlugin : Plugin
    {
        public void OnPluginStart()
        {
            Console.WriteLine(".NET: OnPluginStart");
        }
        
        public void OnPluginUpdate(float dt)
        {
            Console.WriteLine(".NET: OnPluginUpdate");
        }
    
        public void OnPluginEnd()
        {
            Console.WriteLine(".NET: OnPluginEnd");
        }
    }
}
```

## Документация

Для получения полной документации по написанию плагинов на C# (.NET) с использованием фреймворка Plugify, см. [официальную документацию Plugify](https://untrustedmodders.github.io).

## Участие в разработке

Вы можете внести вклад, открыв issue или отправив pull request. Мы будем рады вашим идеям и предложениям!

## Лицензия

Этот модуль языка C# (.NET) для Plugify распространяется по лицензии [MIT](LICENSE).