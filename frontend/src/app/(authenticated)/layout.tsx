"use client";

import { useMe } from "@/src/lib/auth";
import { usePathname, useRouter} from "next/navigation";
import { useEffect } from "react";

const roleMap: Record<string, string> = {
  "/admin": "Admin",
  "/staff": "Staff",
  "/attendee": "Attendee",
  "/superadmin": "SuperAdmin",
};

export default function AuthenticatedLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  const { data: user, isLoading, isError } = useMe();
  const pathname = usePathname();
  const router = useRouter();

  useEffect(() => {
    if (isError) {
      router.replace("/login");
      return;
    }

    if (!isLoading && user) {
      const requiredRole = Object.keys(roleMap).find((p) =>
        pathname.startsWith(p)
      );

      if (!requiredRole || user.role !== roleMap[requiredRole]) {
        router.replace("/login");
      }
    }
  }, [isLoading, user, isError, pathname, router]);

  if (isLoading) return null;

  return <>{children}</>;
}
