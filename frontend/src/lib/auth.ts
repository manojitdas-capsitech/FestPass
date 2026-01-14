import { useQuery } from "@tanstack/react-query";
import api from "./axios";

export type MeResponse = {
  email: string;
  role: "Admin" | "Staff" | "Attendee" | "SuperAdmin";
};

export const fetchMe = async (): Promise<MeResponse> => {
  const res = await api.get("/auth/me", {
    withCredentials: true,
  });
  return res.data;
};

export const useMe = () => {
  return useQuery({
    queryKey: ["me"],
    queryFn: fetchMe,
    retry: false,          // do NOT retry auth failures
    staleTime: 5 * 60 * 1000, // cache for 5 minutes
  });
};
