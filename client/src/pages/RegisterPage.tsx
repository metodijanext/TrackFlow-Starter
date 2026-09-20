import { ChangeEvent, FormEvent, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { useAuth } from '../context/AuthContext'

export default function RegisterPage() {
  const { register, isLoading, error } = useAuth()
  const navigate = useNavigate()
  const [form, setForm] = useState({ firstName: '', lastName: '', email: '', password: '' })

  const update = (field: keyof typeof form) => (e: ChangeEvent<HTMLInputElement>) =>
    setForm((prev) => ({ ...prev, [field]: e.target.value }))

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault()
    try {
      await register(form)
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
          <p className="text-gray-500 mt-1">Направете нова сметка</p>
        </div>

        {error && (
          <div className="mb-4 rounded-md bg-red-50 border border-red-200 text-red-700 text-sm px-4 py-2">
            {error}
          </div>
        )}

        <form onSubmit={handleSubmit} className="space-y-4">
          <div className="grid grid-cols-2 gap-3">
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Име</label>
              <input
                required
                value={form.firstName}
                onChange={update('firstName')}
                className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-2 focus:ring-primary-500"
              />
            </div>
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Презиме</label>
              <input
                required
                value={form.lastName}
                onChange={update('lastName')}
                className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-2 focus:ring-primary-500"
              />
            </div>
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">Е-пошта</label>
            <input
              type="email"
              required
              value={form.email}
              onChange={update('email')}
              className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-2 focus:ring-primary-500"
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">Лозинка</label>
            <input
              type="password"
              required
              minLength={6}
              value={form.password}
              onChange={update('password')}
              className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-2 focus:ring-primary-500"
            />
          </div>
          <button
            type="submit"
            disabled={isLoading}
            className="w-full bg-primary-600 hover:bg-primary-700 disabled:opacity-60 text-white font-medium rounded-md py-2 transition"
          >
            {isLoading ? 'Се регистрира...' : 'Регистрирај се'}
          </button>
        </form>

        <p className="text-sm text-gray-500 mt-4 text-center">
          Веќе имате сметка?{' '}
          <Link to="/login" className="text-primary-600 font-medium hover:underline">
            Најави се
          </Link>
        </p>
      </div>
    </div>
  )
}
