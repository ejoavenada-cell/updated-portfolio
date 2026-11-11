#!/usr/bin/env bash
set -e

echo "==> Installing .NET 10 SDK..."
curl -sSL https://dot.net/v1/dotnet-install.sh | bash -s -- --channel 10.0

export PATH="$PATH:$HOME/.dotnet"
export DOTNET_ROOT="$HOME/.dotnet"

echo "==> Verifying .NET version:"
dotnet --version

echo "==> Building & Publishing Blazor WebAssembly (.NET 10)..."
dotnet publish Portfolio.Wasm/Portfolio.Wasm.csproj -c Release -o dist

# Ensure 404.html is available for SPA client-side routing on any host
cp dist/wwwroot/index.html dist/wwwroot/404.html

# Fallback safeguard: ensure dotnet.js is always reachable at _framework/dotnet.js
if [ ! -f dist/wwwroot/_framework/dotnet.js ]; then
  echo "==> Safeguard: Copying dotnet runtime entrypoint to dist/wwwroot/_framework/dotnet.js"
  cp dist/wwwroot/_framework/dotnet.*.js dist/wwwroot/_framework/dotnet.js 2>/dev/null || cp dist/dotnet.js dist/wwwroot/_framework/dotnet.js
fi

echo "==> Build complete! Output is in dist/wwwroot"
