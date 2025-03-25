## MultiTenantEventManager
# Kurulum

- Veritabanını oluşturmak için aşağıdaki komutu çalıştırın:

> update-database

- Bu işlem, Persistence katmanında bulunan DbContext üzerinden veritabanı ve tabloları oluşturacaktır.

# Projeyi çalıştırın.

- Proje ilk çalıştırıldığında SeedDataService aracılığı ile aşağıdaki veriler otomatik olarak eklenecektir:

> SüperAdmin yetkisinde bir kullanıcı.

> Default Tenant adında bir tenant kaydı.

# Önemli Notlar

- Veritabanı oluşturulmadan önce proje çalıştırılırsa hata alınacaktır. Bu nedenle update-database komutunu çalıştırmadan projeyi başlatmayın.

- Yeni tenant ekleme yetkisi sadece SüperAdmin'e aittir.

- Yeni bir tenant eklendikten sonra, bu tenanta bağlı olarak diğer rollerde kullanıcılar eklenebilir.

- Tenantlara bağlı olarak etkinlikler oluşturulabilir ve bu etkinliklere katılımcı kayıtları eklenebilir.

# Kullanım Akışı

- SüperAdmin, yeni bir tenant oluşturur.

- Tenant oluşturulduktan sonra bu tenant'a bağlı roller belirlenir.

- Roller bazında kullanıcılar eklenir.

- Tenant'a bağlı etkinlikler oluşturulur.

- Etkinliklere katılımcılar eklenir.

Bu proje, çoklu tenant desteği ile etkinlik yönetimi sağlamaktadır. Geliştirme sırasında ihtiyaçlarınıza göre özelleştirme yapabilirsiniz.

