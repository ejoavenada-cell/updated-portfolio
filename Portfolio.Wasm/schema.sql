-- ==========================================
-- 1. TABLES SETUP
-- ==========================================
CREATE TABLE IF NOT EXISTS profile_settings (
  id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
  full_name TEXT NOT NULL DEFAULT 'Erick John',
  headline TEXT NOT NULL DEFAULT '.NET & Full-Stack Software Developer',
  status_text TEXT DEFAULT 'Available for Opportunities',
  bio TEXT DEFAULT 'Building modern, high-performance web applications with Blazor and .NET.',
  avatar_url TEXT,
  resume_url TEXT,
  github_url TEXT,
  linkedin_url TEXT,
  email_address TEXT,
  updated_at TIMESTAMPTZ DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS projects (
  id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
  title TEXT NOT NULL,
  slug TEXT UNIQUE NOT NULL,
  summary TEXT NOT NULL,
  content_markdown TEXT,
  thumbnail_url TEXT,
  gallery_urls TEXT[] DEFAULT '{}',
  tech_tags TEXT[] DEFAULT '{}',
  live_url TEXT,
  repo_url TEXT,
  is_featured BOOLEAN DEFAULT false,
  display_order INT DEFAULT 0,
  created_at TIMESTAMPTZ DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS skills (
  id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
  name TEXT NOT NULL,
  category TEXT NOT NULL, -- 'Backend', 'Frontend', 'Tools'
  icon_name TEXT,
  display_order INT DEFAULT 0
);

CREATE TABLE IF NOT EXISTS experiences (
  id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
  role_or_degree TEXT NOT NULL,
  organization TEXT NOT NULL,
  period TEXT NOT NULL,
  description TEXT,
  type TEXT DEFAULT 'Work', -- 'Work', 'Education', 'Certification'
  display_order INT DEFAULT 0
);

CREATE TABLE IF NOT EXISTS messages (
  id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
  sender_name TEXT NOT NULL,
  sender_email TEXT NOT NULL,
  message TEXT NOT NULL,
  is_read BOOLEAN DEFAULT false,
  created_at TIMESTAMPTZ DEFAULT NOW()
);

-- Insert Default Single Row for Profile Settings
INSERT INTO profile_settings (full_name, headline, status_text)
SELECT 'Erick John', '.NET & Blazor Software Developer', 'Available for Opportunities'
WHERE NOT EXISTS (SELECT 1 FROM profile_settings);

-- ==========================================
-- 2. ROW LEVEL SECURITY (RLS) FOR TABLES
-- ==========================================
ALTER TABLE profile_settings ENABLE ROW LEVEL SECURITY;
ALTER TABLE projects ENABLE ROW LEVEL SECURITY;
ALTER TABLE skills ENABLE ROW LEVEL SECURITY;
ALTER TABLE experiences ENABLE ROW LEVEL SECURITY;
ALTER TABLE messages ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Public read profile" ON profile_settings FOR SELECT USING (true);
CREATE POLICY "Public read projects" ON projects FOR SELECT USING (true);
CREATE POLICY "Public read skills" ON skills FOR SELECT USING (true);
CREATE POLICY "Public read experiences" ON experiences FOR SELECT USING (true);
CREATE POLICY "Public insert messages" ON messages FOR INSERT WITH CHECK (true);

CREATE POLICY "Admin full access profile" ON profile_settings FOR ALL TO authenticated USING (true);
CREATE POLICY "Admin full access projects" ON projects FOR ALL TO authenticated USING (true);
CREATE POLICY "Admin full access skills" ON skills FOR ALL TO authenticated USING (true);
CREATE POLICY "Admin full access experiences" ON experiences FOR ALL TO authenticated USING (true);
CREATE POLICY "Admin full access messages" ON messages FOR ALL TO authenticated USING (true);

-- ==========================================
-- 3. STORAGE BUCKET & UPLOAD POLICIES
-- ==========================================
INSERT INTO storage.buckets (id, name, public)
VALUES ('portfolio-media', 'portfolio-media', true)
ON CONFLICT (id) DO NOTHING;

-- Allow anyone to view uploaded portfolio images
CREATE POLICY "Public View Portfolio Media"
ON storage.objects FOR SELECT
USING (bucket_id = 'portfolio-media');

-- Allow only authenticated Admin to upload/update/delete images
CREATE POLICY "Admin Upload Portfolio Media"
ON storage.objects FOR INSERT TO authenticated
WITH CHECK (bucket_id = 'portfolio-media');

CREATE POLICY "Admin Update Portfolio Media"
ON storage.objects FOR UPDATE TO authenticated
USING (bucket_id = 'portfolio-media');

CREATE POLICY "Admin Delete Portfolio Media"
ON storage.objects FOR DELETE TO authenticated
USING (bucket_id = 'portfolio-media');
