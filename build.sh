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

echo "==> Build complete! Output is in dist/wwwroot"
