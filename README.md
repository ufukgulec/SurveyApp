Başlarken 
=============================
- [Uygulama Hakkında](#anket-web-uygulamasi)
	- [Uygulama Özellikleri](#özellikler)
	- [Uygulama Görüntüleri](#uygulama-görüntüleri)
	- [Kullanılanlar](#kullanılanlar)
- [EntityLayer](#varlık-katmanı---entitylayer)
	- [Örnek Kullanım](#i̇ki-tablo-arasındaki-ilişkiyi-oluşturma-örneği)
- [DataAccessLayer](#veri-erişim-katmanı---dataaccesslayer)
	- [Context.cs](#contextcs)
	- [Migrations](#migrations)
	- [DataAccesLayer Yapısı](#dataacceslayer-yapısı)
- [BusinessLayer](#i̇ş-katmanı---businesslayer)
	- [BusinessLayer Yapısı](#businesslayer-yapısı)
	- [Fluent Validation](#fluent-validation)
- [PresentationLayer](#sunum-katmanı---rys)
	- [Controllers](#controllers)
	- [Models](#models)
	- [Views](#views)
- [Arayüz Tasarımı](#arayüz-tasarımı)

# SurveyApp Uygulaması

Birden çok kullanıcının bir araya gelerek anketler hazırlayabildiği ve anketleri cevaplayabildiği bir uygulamadır. Uygulamada kullanıcı anket oluştururken bir profile sahip olması gerekiyor. Bu profillerin rolleri olacağından dolayı yönetim ve kullanıcı olarak ayrılıyor.

## Proje Tanıtımı

.Net 6 kullanarak oluşturduğum bu projede Onion Architecture mimarisini kullandım. Gençay Yıldız ve Salih Cantekin'in eğitim videolarından yardım alarak geliştirdiğim projede SOLID prensiplerini ve generic repository pattern kullandım.

## Özellikler

Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.
  
## Kullanılanlar

Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.
  
## Veri Tabanı Şeması

Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.

![image](https://user-images.githubusercontent.com/51711890/198718451-a3357f75-f0ec-46af-b2a7-ce3c46bed37c.png)


### İki tablo arasındaki ilişkiyi oluşturma örneği

```javascript
public class Category
{
  public List<Product> Products { get; set; }
}

public class Product
{
  public int CategoryId { get; set; }
  public Category Category { get; set; }
}
```
Bire çok (1-n) ilişkili tablolarda kullanılan yapı böyledir.

## Veri Erişim Katmanı - DataAccessLayer
Projenin veri tabanı ile bağlantı kuran katmanıdır. Concrete klasörü altında Context.cs sınıf bulunur ve veri tabanı bağlantısını tutar. 

### Context.cs

```javascript
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
  optionsBuilder.UseSqlServer("server=UFUK;database=Rys;integrated security=true");
}
```
Code First yaklaşımı ile hazırladığım için Rys adında veri tabanı oluşturur. Veri tabanında tablolarıda bu sınıfta DbSet olarak tutarız.

```javascript
public DbSet<Category> Categories { get; set; }
```

### Migrations

Hazırlanan Context.cs sınıfını veri tabanına yansıtmak için DataAccessLayer'da 

İlk Adım:

```bash
Add-Migration MigrationName
```
DataAccessLayer içinde Migrations klasörünün altında MigrationName adında bir sınıf oluşur. Yapılan değişiklikleri görebilirsiniz.

Sonraki adım:

```bash
Update-Database
```
Yapılan değişiklik veri tabanına yansır.

### DataAccesLayer Yapısı

Abstract klasörünün içine interface sınıflarımı oluşturdum (IGenericDal.cs mesela) repositories klasörü altında oluşturulan GenericRepository.cs sınıfı IGenericDal.cs'den kalıtım alır ve veri tabanı işlemleri (CRUD işlemleri) yapan bir sınıftır. EntityFramework klasörünün içinde oluşturulan varlık repositoryleri (EfCategoryRepository mesela) GenericRepository.cs'den kalıtım alır.

## İş Katmanı - BusinessLayer
Projenin DataAccessLayer ile Sunum katmanını birleştiren katmandır. Validasyon kontrolleri yapar.  

### BusinessLayer Yapısı

Abstract klasörünün içine interface sınıflarımı oluşturdum (IGenericService.cs mesela) Concrete klasörü altında oluşturulan GenericManager.cs sınıfı IGenericService.cs'den kalıtım alır ve veri tabanı işlemleri (CRUD işlemleri) yapan bir sınıftır. EntityFramework klasörünün içinde oluşturulan varlık repositoryleri (EfCategoryRepository mesela) GenericRepository.cs'den kalıtım alır.

### Fluent Validation

ValidationRules klasörü altında varlıkların ekleme çıkarma yaparken kontorllerinin sağlandığı sınıflar vardır. Fluent Validation'ı kullanma amacım kuralları tanımlanmış varlıkların oluşturulurken boş ve geçersiz kullanımlarda hata mesajı dönmesini ve hatalı girdileri veri tabanına yansıtmamak için kullandım. 

```javascript
public class CategoryValidator : AbstractValidator<Category> 
{ 
  public CategoryValidator()
  { 
    RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori adı boş geçilemez"); 
    RuleFor(x => x.Description).MaximumLength(100).WithMessage("100 Karakterden fazla giriş yapmayınız.");
  }
}
```

Kategori adı boş değer olursa "Kategori adı boş geçilemez" mesajı View'e gider.

Controller'da Fluent Validation nasıl kullanılır?

```javascript
CategoryValidator validationRules = new CategoryValidator();
ValidationResult validationResult = validationRules.Validate(category);

validationResult.IsValid -> Boolean değer döner.
```

## Sunum Katmanı - Rys

Projenin sunum katmanı .Net Core5 ile oluşturulmuştur. Model-View-Controller yapısını kullanarak BussinesLayer ve EntityLayer ile ortaklaşa çalışır.

### Controllers

Web projesinin arka planında çalışan kısımdır. BussinesLayer ile bağlantılı çalışır.

Örneğin:

```bash
CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
```
İş katmanında bulunan CategoryManager'ı yeniler. CategoryManager DataAccessLayer'dan Context.cs sınıfını kullanması için EfCategoryRepository.cs sınıfını parametre olarak kullanmalıdır.

### Models

İki ilişkili tabloyu çekerken bazen Include metodu yetersiz kalıyor onun için Models klasörünün içine yeni bir sınıf tanımlanır bu sınıf EntityLayer'daki varlığ kalıtım alır.

Örneğin:

```bash
public class VMPhoneOrder : EntityLayer.Concrete.PhoneOrder
```
Bu klasörde oluşturulan Model View'e gönderilir ve denetleyicide kod karmaşıklığı olmaz.

### Views

Bu klasörün denetleyici isimlerine göre klasörlere ayrılmış bir yapısı vardır. Shared klasörü altında temanın Navbar, Footer, Menu ve Layoutu gibi parçalanmış görünümlerini tutar.

Layout.cshtml nasıl parçalanır?

Temanın Navbarını ayrı dosyada tutmak istiyorsanız Layouta bildirmemiz gerekiyor.

Örneğin:

```bash
<partial name="_Navbar" />
```

View dosyalarında denetleyiciden gelen değerleri okumak için gelen değere göre kod parçacıkları eklemeliyiz.

Denetleyiciden tek bir değer geliyorsa:
```javascript
@using EntityLayer.Concrete;
@model Product
```
Denetleyiciden gelen değer dizi ise:
```javascript
@using EntityLayer.Concrete;
@model List<Product>
```
### Authentication
Kullanıcı adı ve parolaya göre giriş yapılması için sunum katmanında Authorization işlemleri yaptım.
Startup.cs Dosyasında ConfigureServices metoduna eklediklerim
```javascript
public void ConfigureServices(IServiceCollection services)
{
  services.AddMvc(c =>
  {
   var policy = new AuthorizationPolicyBuilder()
			.RequireAuthenticatedUser()
                            .Build();
   c.Filters.Add(new AuthorizeFilter(policy));
   });

   services.AddMvc();
   services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
   {
    x.LoginPath = "/Login/Index";
   });
}
```
