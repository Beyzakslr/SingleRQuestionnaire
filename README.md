# Canlı Anket Platformu (Real-Time Polling App)

.NET 8, SignalR ve MongoDB kullanılarak geliştirilmiş, kullanıcıların gerçek zamanlı olarak anketler oluşturup oylayabildiği bir web uygulamasıdır.

## Proje Hakkında

Bu proje, **.NET SignalR** teknolojisini derinlemesine öğrenme hedefiyle geliştirilmiş olup, modern bir web uygulamasının temel taşlarını bir araya getirmeyi amaçlamaktadır. SignalR'ın gücüyle anlık iletişim sağlanırken, ASP.NET Core Identity ile güvenli kullanıcı yönetimi ve MongoDB ile esnek veri depolama çözümleri sunulmaktadır. Kullanıcılar kendi anketlerini oluşturabilir, linkini paylaşabilir ve sonuçları canlı olarak takip edebilir.

## 🚀 Öne Çıkan Özellikler

- **Gerçek Zamanlı Sonuçlar:** SignalR Hub ve Grupları kullanılarak oylar anında tüm istemcilere yansıtılır.
- **Güvenli Üyelik Sistemi:** ASP.NET Core Identity ile kullanıcı kaydı, girişi ve yetkilendirme.
- **Dinamik Anket Yönetimi:** Kullanıcılar kendi sorularını ve seçeneklerini belirleyerek anket oluşturabilir.
- **Tek Oy Hakkı:** Her kullanıcının bir ankete sadece bir kez oy vermesi sağlanmıştır.
- **NoSQL Veritabanı:** Tüm veriler MongoDB üzerinde esnek bir yapıda saklanır.

## 🛠️ Teknoloji Stack'i

- **Backend:** .NET 8, ASP.NET Core (MVC & Razor Pages), SignalR
- **Veritabanı:** MongoDB (MongoDB.Driver ile)
- **Kimlik Doğrulama:** ASP.NET Core Identity 
- **Frontend:** HTML, CSS, Bootstrap, JavaScript
