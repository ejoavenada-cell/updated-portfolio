# Erick John — .NET 10 Blazor WebAssembly Portfolio

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Blazor](https://img.shields.io/badge/Blazor-WebAssembly-512BD4?logo=blazor)](https://blazor.net)
[![Supabase](https://img.shields.io/badge/Supabase-PostgreSQL%20%2B%20RLS-3ECF8E?logo=supabase)](https://supabase.com)
[![Tailwind CSS](https://img.shields.io/badge/Tailwind_CSS-v4-38B2AC?logo=tailwind-css)](https://tailwindcss.com)

A high-performance personal portfolio built with **Blazor WebAssembly (.NET 10)**, featuring a cyber-navy glassmorphic UI, Markdig-rendered technical case studies, Supabase Row-Level Security (RLS), and a full-featured admin control plane.

---

## ✨ Features

- **⚡ Blazor WebAssembly (.NET 10)**: Fast client-side C# execution with Brotli compression.
- **🎨 Cyber-Navy & Electric Cyan Theme**: Obsidian (`#05070B`), Deep Navy (`#0A0F1D`), and Electric Cyan (`#00F0FF`) glassmorphic UI.
- **⌨️ Command Palette (`Ctrl + K` / `⌘K`)**: Instant fuzzy search across projects, tech stack, and navigation nodes.
- **📝 Markdown Case Studies**: Technical project deep-dives parsed in real-time with `Markdig`.
- **🖼️ In-Browser WebP Image Pipeline**: Images are auto-resized and converted to WebP (`1600x900`) before uploading to Supabase Storage.
- **🔒 Supabase Database & Auth**: Direct PostgreSQL access powered by `supabase-csharp` and Row-Level Security (RLS).
- **🛡️ Admin Control Plane**:
  - Live Profile & Bio editor with avatar uploader
  - Projects CRUD with live preview links and tech tags
  - Skills index & career timeline manager
  - Visitor inquiries inbox with read/unread toggles

---

## 🚀 Quick Start (Local Development)

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

### Run Locally
```bash
# Clone the repository
git clone https://github.com/YOUR_USERNAME/Erick-Portfolio.git
cd Erick-Portfolio

# Run with hot-reload
dotnet watch --project Portfolio.Wasm
```
Navigate to `http://localhost:5267` in your browser.

---

## 🗄️ Supabase Configuration

1. Copy the contents of `schema.sql` and run them in your **Supabase Dashboard > SQL Editor**.
2. Configure your Supabase URL and public anonymous key in `Portfolio.Wasm/wwwroot/appsettings.json`:
   ```json
   {
     "Supabase": {
       "Url": "https://YOUR_PROJECT_ID.supabase.co",
       "AnonKey": "YOUR_SUPABASE_ANON_KEY"
     }
   }
   ```
3. Create an admin user under **Authentication > Users** in Supabase, then sign in at `/login`.

---

## 🌐 Deployment to Vercel

This repository includes `vercel.json` and `build.sh` pre-configured to install the .NET 10 SDK and compile the static WASM bundle on Vercel:

1. Import this repository in [Vercel](https://vercel.com/new).
2. Leave the build settings as default (Vercel automatically detects `vercel.json`).
3. Click **Deploy**.

---

## 📄 License
MIT License. Created by Erick John.
