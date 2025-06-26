# C# (.NET) Language Module for Plugify

The C# (.NET) Language Module for Plugify facilitates the development of plugins in C# for the Plugify framework. With this module, you can seamlessly integrate C# plugins, allowing dynamic loading and management by the Plugify core.

## Features

- **C# (.NET) Plugin Support**: Write your plugins in C# (.NET) and seamlessly integrate them with the Plugify framework.
- **Automatic Exporting**: Effortlessly export and import methods between plugins and the language module.
- **Initialization and Cleanup**: Handle plugin initialization, startup, and cleanup with dedicated module events.
- **Interoperability**: Communicate with plugins written in other languages through auto-generated interfaces.

**Note**: All C# (.NET) plugins are hosted within the single domain. This allows for seamless collaboration and interaction between C# plugins without the Plugify framework.

## Getting Started

### Prerequisites

- .NET Runtime [(.NET 9.0.0)](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- Plugify Framework Installed

### Installation

#### Option 1: Install via Plugify Plugin Manager

You can install the C++ Language Module using the Plugify plugin manager by running the following command:

```bash
plg install plugify-module-dotnet
```

#### Option 2: Manual Installation

1. Install dependencies:  

   a. Windows
   > Setting up [CMake tools with Visual Studio Installer](https://learn.microsoft.com/en-us/cpp/build/cmake-projects-in-visual-studio#installation)

   b. Linux:
   ```sh
   sudo apt-get install -y build-essential cmake ninja-build
   ```
   
   c. Mac:
   ```sh
   brew install cmake ninja
   ```

2. Clone this repository:

    ```bash
    git clone https://github.com/untrustedmodders/plugify-module-dotnet.git --recursive
    cd plugify-module-dotnet
    ```

3. Build the C# (.NET) language module:

    ```bash
    mkdir build && cd build
    cmake ..
    cmake --build .
    ```

### Usage

1. **Integration with Plugify**

   Ensure that your C# (.NET) language module is available in the same directory as your Plugify setup.

2. **Write C# Plugins**

   Develop your plugins in C# using the Plugify C# API. Refer to the [Plugify C# Plugin Guide](https://untrustedmodders.github.io/en/developing/plugins/first-plugin) for detailed instructions.

3. **Build and Install Plugins**

   Compile your C# plugins and place the assemblies in a directory accessible to the Plugify core.

4. **Run Plugify**

   Start the Plugify framework, and it will dynamically load your C# plugins.

## Example

```c#
using Plugify;

namespace ExamplePlugin
{
    public class SamplePlugin : Plugin
    {
        public void OnPluginStart()
        {
            Console.WriteLine(".NET: OnStart");
        }
        
        public void OnPluginUpdate()
        {
            Console.WriteLine(".NET: OnStart");
        }
    
        public void OnPluginEnd()
        {
            Console.WriteLine(".NET: OnEnd");
        }
    }
}
```

## Documentation

For comprehensive documentation on writing plugins in C# (.NET) using the Plugify framework, refer to the [Plugify Documentation](https://untrustedmodders.github.io).

## Contributing

Feel free to contribute by opening issues or submitting pull requests. We welcome your feedback and ideas!

## License

This C# (.NET) Language Module for Plugify is licensed under the [MIT License](LICENSE).
