import { createContext, useContext, useState, ReactNode } from 'react'
import type { UserDto, LoginRequest, RegisterRequest } from '../types/auth'
import { authApi } from '../api/authApi'

interface AuthContextValue {
  user: UserDto | null
  isAuthenticated: boolean
  isLoading: boolean
  error: string | null
  login: (data: LoginRequest) => Promise<void>
  register: (data: RegisterRequest) => Promise<void>
  logout: () => void
}

const AuthContext = createContext<AuthContextValue | undefined>(undefined)

function readStoredUser(): UserDto | null {
  const raw = localStorage.getItem('user')
  return raw ? (JSON.parse(raw) as UserDto) : null
}

export function AuthProvider({ children }: { children: ReactNode }) {
  const [user, setUser] = useState<UserDto | null>(readStoredUser())
  const [isLoading, setIsLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)

  const persistSession = (accessToken: string, refreshToken: string, userDto: UserDto) => {
    localStorage.setItem('accessToken', accessToken)
    localStorage.setItem('refreshToken', refreshToken)
    localStorage.setItem('user', JSON.stringify(userDto))
    setUser(userDto)
  }

  const login = async (data: LoginRequest) => {
    setIsLoading(true)
    setError(null)
    try {
      const res = await authApi.login(data)
      persistSession(res.accessToken, res.refreshToken, res.user)
    } catch (err: any) {
      setError(err?.response?.data?.message || 'Најавата не успеа. Проверете ги податоците.')
      throw err
    } finally {
      setIsLoading(false)
    }
  }

  const register = async (data: RegisterRequest) => {
    setIsLoading(true)
    setError(null)
    try {
      const res = await authApi.register(data)
      persistSession(res.accessToken, res.refreshToken, res.user)
    } catch (err: any) {
      setError(err?.response?.data?.message || 'Регистрацијата не успеа.')
      throw err
    } finally {
      setIsLoading(false)
    }
  }

  const logout = () => {
    localStorage.removeItem('accessToken')
    localStorage.removeItem('refreshToken')
    localStorage.removeItem('user')
    setUser(null)
  }

  return (
    <AuthContext.Provider
      value={{ user, isAuthenticated: !!user, isLoading, error, login, register, logout }}
    >
      {children}
    </AuthContext.Provider>
  )
}

export function useAuth() {
  const ctx = useContext(AuthContext)
  if (!ctx) throw new Error('useAuth must be used within an AuthProvider')
  return ctx
}
