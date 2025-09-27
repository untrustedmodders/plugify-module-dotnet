#!/bin/bash
# build.sh - For Linux builds

set -ex

# Detect the platform and set the appropriate library extension
if [[ "$OSTYPE" == "darwin"* ]]; then
    LIB_EXT="dylib"
elif [[ "$OSTYPE" == "linux-gnu"* ]]; then
    LIB_EXT="so"
else
    echo "Unsupported platform: $OSTYPE"
    exit 1
fi

# Create the target directories
mkdir -p $PREFIX/bin
mkdir -p $PREFIX/api
mkdir -p $PREFIX/dotnet
mkdir -p $PREFIX

# Copy the shared library and module file
cp bin/libplugify-module-dotnet.$LIB_EXT $PREFIX/bin/
cp -r api/* $PREFIX/api/
cp -r dotnet/* $PREFIX/dotnet/
cp plugify-module-dotnet.pmodule $PREFIX/

# Set proper permissions
chmod 755 $PREFIX/bin/libplugify-module-dotnet.$LIB_EXT
chmod -R 755 $PREFIX/api
chmod -R 755 $PREFIX/dotnet
chmod 644 $PREFIX/plugify-module-dotnet.pmodule

# Create activation scripts for proper library path
mkdir -p $PREFIX/etc/conda/activate.d
mkdir -p $PREFIX/etc/conda/deactivate.d

cat > $PREFIX/etc/conda/activate.d/plugify-module-dotnet.sh << EOF
#!/bin/bash
export PLUGIFY_NET_MODULE_PATH="\${CONDA_PREFIX}:\${PLUGIFY_NET_MODULE_PATH}"
EOF

cat > $PREFIX/etc/conda/deactivate.d/plugify-module-dotnet.sh << EOF
#!/bin/bash
export PLUGIFY_NET_MODULE_PATH="\${PLUGIFY_NET_MODULE_PATH//\${CONDA_PREFIX}:/}"
EOF

chmod +x $PREFIX/etc/conda/activate.d/plugify-module-dotnet.sh
chmod +x $PREFIX/etc/conda/deactivate.d/plugify-module-dotnet.sh