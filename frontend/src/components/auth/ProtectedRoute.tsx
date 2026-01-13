"use client";
import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import api from "@/src/lib/axios";

export default function ProtectedRoute({
  children,
  roles,
}: {
  children: React.ReactNode;
  roles: string[];
}) {
  const router = useRouter();
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const checkAuth = async () => {
      try {
        const res = await api.get("/auth/me"); // Cookie JWT auto sent
        const userRole = res.data.role;

        if (!roles.includes(userRole)) {
          router.replace("/login");
          return;
        }

        setLoading(false);
      } catch {
        router.replace("/login");
      }
    };

    checkAuth();
  }, [router, roles]);

  if (loading) return null; // or spinner

  return <>{children}</>;
}
