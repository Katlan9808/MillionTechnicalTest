'use client';
import React from 'react';

export default function WelcomeMessage() {
  const currentHour = new Date().getHours();
  const greeting =
    currentHour < 12 ? 'Good morning' :
    currentHour < 18 ? 'Good afternoon' :
    'Good evening';

  return (
    <div className="bg-gradient-to-r from-blue-600 to-indigo-700 text-white p-6 rounded-lg shadow-md">
      <h1 className="text-3xl font-bold mb-2">{greeting} 👋</h1>
      <p className="text-lg">
        Welcome back to your real estate dashboard. Let’s build something great today.
      </p>
    </div>
  );
}