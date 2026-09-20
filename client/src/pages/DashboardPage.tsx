import { useAuth } from '../context/AuthContext'
import { useNavigate } from 'react-router-dom'

export default function DashboardPage() {
  const { user, logout } = useAuth()
  const navigate = useNavigate()

  const handleLogout = () => {
    logout()
    navigate('/login')
  }

  return (
    <div className="min-h-screen bg-gray-50">
      <header className="bg-white shadow-sm">
        <div className="max-w-5xl mx-auto px-6 py-4 flex items-center justify-between">
          <h1 className="text-xl font-bold text-gray-900">TrackFlow 📊</h1>
          <div className="flex items-center gap-4">
            <span className="text-sm text-gray-600">
              {user?.firstName} {user?.lastName} <span className="text-gray-400">({user?.role})</span>
            </span>
            <button
              onClick={handleLogout}
              className="text-sm text-red-600 hover:underline font-medium"
            >
              Одјави се
            </button>
          </div>
        </div>
      </header>

      <main className="max-w-5xl mx-auto px-6 py-10">
        <div className="bg-white rounded-xl shadow-sm p-8">
          <h2 className="text-lg font-semibold text-gray-900 mb-2">
            Добредојде, {user?.firstName}! 👋
          </h2>
          <p className="text-gray-500">
            Ова е почетна контролна табла на TrackFlow. Најавата и регистрацијата работат
            преку вистинско JWT автентицирање со бекендот.
          </p>
          <div className="mt-6 rounded-lg border border-dashed border-gray-300 p-6 text-center text-gray-400">
            Функционалностите за проекти и внесови за време (Time Entries) ќе се
            развиваат тука во текот на курсот — почнувајќи од Недела 2 (Projects) и
            Недела 4 (Time Entries CRUD).
          </div>
        </div>
      </main>
    </div>
  )
}
