"use client"

import { useRouter } from "next/navigation"
import { FaTicket } from "react-icons/fa6";
import { Divider, Stat } from "../components/common/Icons";

export default function Hero() {

  const router = useRouter();  

  return (
    <div>
      <div className="hero-gradient min-h-screen w-screen px-20 py-12 flex flex-col justify-between text-white">
        <div className="flex justify-between items-center">
          {/* Brand */}
          <div className="flex items-center gap-3">
            <div className="h-9 w-9 rounded-lg bg-linear-to-br from-violet-500 to-violet-700 flex items-center justify-center font-bold">
              <FaTicket size={20} />
            </div>
            <span className="text-2xl font-semibold">FestPass</span>
          </div>

          <button onClick={()=>router.push("/login")} className="bg-linear-to-br from-violet-500 to-violet-700 text-white px-2 py-1 border-none text-xl rounded-xl cursor-pointer">Login</button>
        </div>

        {/* Content */}
        <div className="mx-auto">
          <h1 className="text-[56px] leading-tight font-extrabold">
            Digital Ticketing <br />
            <span className="text-violet-500">Made Simple</span>
          </h1>

          <p className="mt-6 text-gray-400 max-w-xl">
            Streamlined access control for college fests. Fast check-ins,
            real-time analytics, zero hassle.
          </p>
        </div>

        {/* Stats */}
        <div className="flex justify-end items-center gap-10">
          <Stat value="50K+" label="Tickets Scanned" />
          <Divider />
          <Stat value="120+" label="Events Managed" />
          <Divider />
          <Stat value="99.9%" label="Uptime" />
        </div>
      </div>
    </div>
  );
}

