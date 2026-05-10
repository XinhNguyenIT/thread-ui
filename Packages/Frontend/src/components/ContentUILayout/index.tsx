export default function ContentUILayout({ children } : { children: React.ReactNode }) {
    return (
        <main className="flex-1 w-full flex justify-center py-1 h-full bg-[#fbfbfb]">
            <div className="w-[800px] h-full border border-zinc-100 rounded-[32px] bg-white shadow-sm flex flex-col overflow-hidden">
                <div className="flex-1 overflow-y-auto p-6 scrollbar-hide">
                    {children}
                </div>
            </div>
        </main>
    )
}