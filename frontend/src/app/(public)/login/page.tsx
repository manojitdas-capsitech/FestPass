"use client";
import { login } from "@/src/services/auth.services";

export default function Login() {

  const handleLogin = async () => {
    const res = await login("superadmin@festpass.com", "welcome");
  };

  return (
    <div className="h-screen w-screen flex justify-center items-center">
      <button
        onClick={handleLogin}
        className="w-2xl bg-black text-white px-2 py-1 cursor-pointer"
      >
        Login
      </button>
      
    </div>
  );
}
