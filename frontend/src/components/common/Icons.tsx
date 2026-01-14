export function Stat({ value, label }: { value: string; label: string }) {
  return (
    <div>
      <p className="text-3xl font-bold">{value}</p>
      <p className="text-sm text-gray-400">{label}</p>
    </div>
  );
}

export function Divider() {
  return <div className="h-10 w-px bg-white/20" />;
}


export function OAuthButton({ label, icon }: { label: string; icon: React.ReactNode }) {
  return (
    <button className="flex-1 flex items-center justify-center gap-3 rounded-lg border border-white/10 py-3 hover:bg-white/5 transition">
      <span className="font-semibold">{icon}</span>
      <span className="text-sm">{label}</span>
    </button>
  );
}