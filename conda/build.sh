#!/bin/bash
# build.sh - For Linux and macOS builds

set -ex

if [[ "$target_platform" == osx-* ]]; then
    LIB_EXT="dylib"
else
    LIB_EXT="so"
fi

mkdir -p $PREFIX/bin
mkdir -p $PREFIX/api
mkdir -p $PREFIX/dotnet

cp bin/libplugify-module-dotnet.$LIB_EXT $PREFIX/bin/
cp -r api/* $PREFIX/api/
cp -r dotnet/* $PREFIX/dotnet/
cp plugify-module-dotnet.pmodule $PREFIX/

chmod 755 $PREFIX/bin/libplugify-module-dotnet.$LIB_EXT
chmod -R 755 $PREFIX/api
chmod -R 755 $PREFIX/dotnet
chmod 644 $PREFIX/plugify-module-dotnet.pmodule

# Create activation scripts for proper library path
mkdir -p $PREFIX/etc/conda/activate.d
mkdir -p $PREFIX/etc/conda/deactivate.d

cat > $PREFIX/etc/conda/activate.d/plugify-module-dotnet.sh << EOF2
#!/bin/bash
export PLUGIFY_NET_MODULE_PATH="\${CONDA_PREFIX}:\${PLUGIFY_NET_MODULE_PATH}"
EOF2

cat > $PREFIX/etc/conda/deactivate.d/plugify-module-dotnet.sh << EOF2
#!/bin/bash
export PLUGIFY_NET_MODULE_PATH="\${PLUGIFY_NET_MODULE_PATH//\${CONDA_PREFIX}:/}"
EOF2

chmod +x $PREFIX/etc/conda/activate.d/plugify-module-dotnet.sh
chmod +x $PREFIX/etc/conda/deactivate.d/plugify-module-dotnet.sh
