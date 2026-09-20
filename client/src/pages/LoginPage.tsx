import { FormEvent, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { useAuth } from '../context/AuthContext'

export default function LoginPage() {
  const { login, isLoading, error } = useAuth()
  const navigate = useNavigate()
  const [email, setEmail] = useState('admin@trackflow.local')
  const [password, setPassword] = useState('')

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault()
    try {
      await login({ email, password })
      navigate('/dashboard')
    } catch {
      // error is surfaced via context
    }
  }

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-50 px-4">
      <div className="w-full max-w-md bg-white rounded-xl shadow-lg p-8">
        <div className="text-center mb-8">
          <h1 className="text-2xl font-bold text-gray-900">TrackFlow 📊</h1>
          <p className="text-gray-500 mt-1">Најавете се на вашата сметка</p>
        </div>

        {error && (
          <div className="mb-4 rounded-md bg-red-50 border border-red-200 text-red-700 text-sm px-4 py-2">
            {error}
          </div>
        )}

        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">Е-пошта</label>
            <input
              type="email"
              required
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-2 focus:ring-primary-500"
              placeholder="you@example.com"
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">Лозинка</label>
            <input
              type="password"
              required
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-2 focus:ring-primary-500"
              placeholder="••••••••"
            />
          </div>
          <button
            type="submit"
            disabled={isLoading}
            className="w-full bg-primary-600 hover:bg-primary-700 disabled:opacity-60 text-white font-medium rounded-md py-2 transition"
          >
            {isLoading ? 'Најавување...' : 'Најави се'}
          </button>
        </form>

        <p className="text-sm text-gray-500 mt-4 text-center">
          Немате сметка?{' '}
          <Link to="/register" className="text-primary-600 font-medium hover:underline">
            Регистрирај се
          </Link>
        </p>

        <p className="text-xs text-gray-400 mt-6 text-center">
          Тест сметка: admin@trackflow.local / Admin123!
        </p>
      </div>
    </div>
  )
}
