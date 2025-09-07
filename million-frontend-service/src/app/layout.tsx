import Link from 'next/link';
import './globals.css';

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="en">
      <body>
        <nav className="bg-gray-800 text-white p-4">
          <Link href="/properties" className="mr-4">Properties</Link>
          <Link href="/owners" className="mr-4">Owners</Link>
        </nav>
        <main className="max-w-4xl mx-auto mt-6">{children}</main>
      </body>
    </html>
  );
}
