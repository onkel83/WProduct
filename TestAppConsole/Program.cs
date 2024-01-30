﻿
using WPAZV.Model;
using WPAZV.Repository;

var mitarbeiterRepository = new MitarbeiterRepository();

            // Add
            Employee mitarbeiter1 = new Employee { ID = 1, LastName = "Mustermann", FirstName = "Max", Birthday = new DateTime(1990, 12, 24), PhoneNumber = "0123456789", Gender = 'M' };
            mitarbeiterRepository.Add(mitarbeiter1);

            Employee mitarbeiter2 = new Employee { ID = 2, LastName = "Mustermann", FirstName = "Moritz", Birthday = new DateTime(1991, 3, 14), PhoneNumber = "0123456789", Gender = 'M' };
            mitarbeiterRepository.Add(mitarbeiter2);

            // Edit
            mitarbeiter1.LastName = "Musterfrau";
            mitarbeiter1.FirstName = "Maria";
            mitarbeiter1.Birthday = new DateTime(1992, 4, 12);
            mitarbeiterRepository.Edit(mitarbeiter1);

            // Delete
            mitarbeiterRepository.Delete(2);

            // GetAll
            List<Employee> mitarbeiterList = mitarbeiterRepository.GetAll();
            foreach (Employee _mitarbeiter in mitarbeiterList){
                Console.WriteLine($"ID: {_mitarbeiter.ID}, Nachname: {_mitarbeiter.LastName}, Vorname: {_mitarbeiter.FirstName}, Geburstag: {_mitarbeiter.Birthday}, Handynummer: {_mitarbeiter.PhoneNumber}, Geschlecht: {_mitarbeiter.Gender}");
            }

            // GetByID
            Employee? mitarbeiter = mitarbeiterRepository.GetByID(1);
            if(mitarbeiter != null)
                Console.WriteLine($"ID: {mitarbeiter.ID}, Nachname: {mitarbeiter.LastName}, Vorname: {mitarbeiter.FirstName}, Geburstag: {mitarbeiter.Birthday}, Handynummer: {mitarbeiter.PhoneNumber}, Geschlecht: {mitarbeiter.Gender}");

            // SaveToXml
            mitarbeiterRepository.SaveToXml("mitarbeiter.xml");

            Console.ReadLine();