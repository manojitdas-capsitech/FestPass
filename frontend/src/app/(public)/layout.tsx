"use client";

import { useMe } from "@/src/lib/auth";
import { useRouter } from "next/navigation";
import { useEffect } from "react";

export default function PublicLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  const { data: user, isLoading } = useMe();
  const router = useRouter();

  useEffect(() => {
    if (!isLoading && user) {
      switch (user.role) {
        case "Admin":
          router.replace("/admin");
          break;
        case "Staff":
          router.replace("/staff");
          break;
        case "Attendee":
          router.replace("/attendee");
          break;
        case "SuperAdmin":
          router.replace("/superadmin");
          break;
        default:
          router.replace("/login");
      }
    }
  }, [user, isLoading, router]);

  // Prevent flicker
  if (isLoading || user) return null;

  return <>{children}</>;
}
