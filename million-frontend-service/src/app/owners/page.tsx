'use client';
import { useEffect, useState } from 'react';
import api from '@/lib/api';
import { OwnerDTO } from '@/types/owner';

export default function OwnersPage() {
  const [owners, setOwners] = useState<OwnerDTO[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    api.get('/owner')
      .then(res => {
        setOwners(res.data.result);
      })
      .catch(err => console.error(err))
      .finally(() => setLoading(false));
  }, []);

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold mb-4">Owners</h1>
      {loading ? (
        <p>Loading...</p>
      ) : (
        <ul className="space-y-4">
          {owners.map(owner => (
            <li key={owner.idOwner} className="border p-4 rounded shadow">
              <h2 className="text-xl font-semibold">{owner.name}</h2>
              <p>{owner.address}</p>
              <p>Birthday: {new Date(owner.birthday).toLocaleDateString()}</p>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}
