"use client";

import { OAuthButton } from "@/src/components/common/Icons";
import { useState } from "react";
import { Button, Form, Input } from "antd";
import { FaEye, FaEyeSlash, FaGithub, FaGoogle } from "react-icons/fa6";
import { useLogin } from "@/src/services/auth/auth.hooks";

export default function Login() {
  const [showPassword, setShowPassword] = useState(false);

  const [form] = Form.useForm();

  const { mutate: login, isPending } = useLogin();

  const handleLogin = async (values: { email: string; password: string }) => {
    login(values);
  };

  return (
    <div className="login-gradient min-h-screen flex items-center justify-center text-white">
      <div className="bg-black w-full max-w-md px-6 py-6 rounded-4xl">
        {/* Header */}
        <div className="mb-10">
          <h1 className="text-3xl font-bold">Welcome back</h1>
          <p className="text-gray-400 mt-2">Sign in to access your dashboard</p>
        </div>

        {/* Form */}
        <Form
          form={form}
          layout="vertical"
          onFinish={handleLogin}
          requiredMark={false}
        >
          {/* Email */}
          <Form.Item
            label={<span className="text-white">Email</span>}
            name="email"
            rules={[
              { required: true, message: "Email is required" },
              { type: "email", message: "Enter a valid email" },
            ]}
          >
            <Input placeholder="you@college.edu" className="input-field" />
          </Form.Item>

          {/* Password */}
          <Form.Item
            label={<span className="text-white">Password</span>}
            name="password"
            rules={[{ required: true, message: "Password is required" }]}
          >
            <Input
              type={showPassword ? "text" : "password"}
              placeholder="••••••••"
              className="input-field pr-12"
              suffix={
                <span
                  onClick={() => setShowPassword((prev) => !prev)}
                  className="cursor-pointer text-gray-400 select-none"
                >
                  {showPassword ? <FaEyeSlash /> : <FaEye />}
                </span>
              }
            />
          </Form.Item>

          <div className="flex justify-end mb-4">
            <span className="text-sm text-violet-500 cursor-pointer hover:underline">
              Forgot password?
            </span>
          </div>

          {/* Sign in */}
          <Form.Item>
            <Button
              htmlType="submit"
              type="primary"
              loading={isPending}
              className="w-full h-12 bg-violet-600 hover:bg-violet-700 border-none font-semibold"
            >
              Sign in
            </Button>
          </Form.Item>
        </Form>

        {/* Divider */}
        <div className="flex items-center gap-4 my-8">
          <div className="h-px flex-1 bg-white/10" />
          <span className="text-sm text-gray-400">or continue with</span>
          <div className="h-px flex-1 bg-white/10" />
        </div>

        {/* OAuth */}
        <div className="flex gap-4">
          <OAuthButton label="Google" icon={<FaGoogle />} />
          <OAuthButton label="GitHub" icon={<FaGithub />} />
        </div>

        {/* Footer */}
        <p className="text-center text-sm text-gray-400 mt-8">
          Don&apos;t have an account?{" "}
          <a href="/signup" className="text-violet-500 hover:underline">
            Sign up
          </a>
        </p>
      </div>
    </div>
  );
}
