'use client';
import { useForm } from 'react-hook-form';
import { z } from 'zod';
import { zodResolver } from '@hookform/resolvers/zod';
import api from '@/lib/api';
import { PropertyDTO } from '@/types/property';
import { useEffect } from 'react';

const schema = z.object({
  name: z.string().min(1),
  address: z.string().min(1),
  price:  z.string().min(1),
  codeInternal: z.string().min(1),
  year: z.string().min(4),
  idOwner: z.string().min(1),
});

type FormData = z.infer<typeof schema>;

export default function PropertyForm({ initialData, onSuccess }: {
  initialData?: PropertyDTO;
  onSuccess?: () => void;
})  {


  const defaultProperty: FormData = {
  name: '',
  address: '',
  price: '0',
  codeInternal: '',
  year: new Date().getFullYear().toString(),
  idOwner: '',
};


  const { register, handleSubmit, formState: { errors }, reset  } = useForm<FormData>({
    resolver: zodResolver(schema),
  });

  useEffect(() => {
    if (initialData) {
      const transformed: FormData = {
      name: initialData.name,
      address: initialData.address,
      price: initialData.price.toString(),
      codeInternal: initialData.codeInternal,
      year: initialData.year.toString(),
      idOwner: initialData.idOwner,
    };
    reset(transformed);

    } else {
      reset(defaultProperty);
    }
  }, [initialData]);


 const onSubmit = async (data: FormData) => {
    try {
      if (initialData) {
        await api.put(`/property/${initialData.idProperty}`, {idProperty:initialData.idProperty, ...data});
        alert('Property updated!');
      } else {
        await api.post('/property', { idProperty: crypto.randomUUID(), ...data });
        alert('Property created!');
      }
      reset();
      onSuccess?.();
    } catch (err) {
      console.error(err);
      alert('Error saving property');
    }
  };


  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-4 p-4 border rounded shadow">
      <input {...register('name')} placeholder="Name" className="input" />
      {errors.name && <p className="text-red-500">{errors.name.message}</p>}

      <input {...register('address')} placeholder="Address" className="input" />
      {errors.address && <p className="text-red-500">{errors.address.message}</p>}

      <input type="number" {...register('price')} placeholder="Price" className="input" />
      {errors.price && <p className="text-red-500">{errors.price.message}</p>}

      <input {...register('codeInternal')} placeholder="Code Internal" className="input" />
      {errors.codeInternal && <p className="text-red-500">{errors.codeInternal.message}</p>}

      <input type="number" {...register('year')} placeholder="Year" className="input" />
      {errors.year && <p className="text-red-500">{errors.year.message}</p>}

      <input {...register('idOwner')} placeholder="Owner ID" className="input" />
      {errors.idOwner && <p className="text-red-500">{errors.idOwner.message}</p>}

      <button type="submit" className="mt-4 bg-green-600 text-white px-4 py-2 rounded">
        {initialData ? 'Update Property' : 'Create Property'}
      </button>

    </form>
  );
}