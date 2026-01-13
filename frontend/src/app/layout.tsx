import type { Metadata } from "next";
import "./globals.css";
import Providers from "../providers/Providers";

export const metadata: Metadata = {
  title: "FestPass",
  description: "Your gateway to unforgettable festival experiences.",
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html>
      <body>
        <Providers>{children}</Providers>
      </body>
    </html>
  );
}
