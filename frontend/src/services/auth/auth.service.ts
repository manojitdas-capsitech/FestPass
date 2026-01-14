import api from "@/src/lib/axios";
import { LoginRequest, LoginResponse } from "./auth";

export const loginService = async (
  payload: LoginRequest
): Promise<LoginResponse> => {
  const res = await api.post<LoginResponse>("/auth/login", payload);
  return res.data;
};

export const logoutService = async () => {
    const res = await api.post("/auth/logout");
    return res.data;
}