import ProtectedRoute from "@/src/components/auth/ProtectedRoute";



export default function SuperAdminPage() {
  return (
    <ProtectedRoute roles={["SuperAdmin"]}>
      <h1>Super Admin Dashboard</h1>
    </ProtectedRoute>
  );
}