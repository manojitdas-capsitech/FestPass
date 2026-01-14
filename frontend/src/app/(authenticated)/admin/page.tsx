"use client"
import { useLogout } from '@/src/services/auth/auth.hooks'

const AdminPage = () => {

  const { mutate: logout } = useLogout();

  return (
    <div>
      <h2>admin page</h2>
      <button onClick={() => logout()}>Logout</button>
    </div>
  )
}

export default AdminPage