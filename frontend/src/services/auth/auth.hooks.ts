"use client";

import { useMutation } from "@tanstack/react-query";
import { useRouter } from "next/navigation";
import toast from "react-hot-toast";
import { LoginRequest, LoginResponse } from "./auth";
import { loginService, logoutService } from "./auth.service";

const roleRedirectMap: Record<LoginResponse["role"], string> = {
    SuperAdmin: "/superadmin",
    Admin: "/admin",
    Staff: "/staff",
    Attendee: "/attendee",
};

export const useLogin = () => {
    const router = useRouter();

    return useMutation<LoginResponse, Error, LoginRequest>({
        mutationFn: loginService,

        onSuccess: (data) => {
            toast.success("Login Successful");
            router.replace(roleRedirectMap[data.role]);
        },

        onError: (error: Error) => {
            toast.error(error?.message || "Invalid email or password");
        },
    });
};

export const useLogout = () => {

    const router = useRouter();

    return useMutation<Error>({
        mutationFn: logoutService,

        onSuccess: () => {
            toast.success("Logout Successfull");
            router.replace("/");
        }
    })
}
