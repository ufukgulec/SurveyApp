﻿using Microsoft.EntityFrameworkCore;
using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Persistence.Contexts
{
    public class SurveyAppContext : DbContext
    {
        public SurveyAppContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User ve Profile arasındaki birebir ilişki
            modelBuilder.Entity<Profile>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(u => u.User)
                .HasForeignKey<Profile>(u => u.Id);
            #endregion

            #region Country ve Profile arasındaki bire çok ilişki
            modelBuilder.Entity<Profile>()
                .HasOne(p => p.Country)
                .WithMany(c => c.Profiles)
                .HasForeignKey(p => p.CountryId);
            #endregion

            #region Role ve Profile arasındaki bire çok ilişki
            modelBuilder.Entity<Profile>()
                .HasOne(p => p.Role)
                .WithMany(c => c.Profiles)
                .HasForeignKey(p => p.RoleId);
            #endregion

            #region User ve Survey arasındaki bire çok ilişki - Anket Oluşturan
            modelBuilder.Entity<Survey>()
                .HasOne(s => s.User)
                .WithMany(u => u.Surveys)
                .HasForeignKey(s => s.UserId);
            #endregion

            #region User ve Survey arasındaki çoka çok ilişki - Ankete Katılan
            #region User ve Participant arasındaki bire çok ilişki
            modelBuilder.Entity<Participant>()
                    .HasOne(p => p.User)
                    .WithMany(u => u.Participants)
                    .HasForeignKey(p => p.UserId);
            #endregion

            #region Survey ve Participant arasındaki bire çok ilişki
            modelBuilder.Entity<Participant>()
                    .HasOne(s => s.Survey)
                    .WithMany(u => u.Participants)
                    .HasForeignKey(s => s.SurveyId);
            #endregion
            #endregion

            #region Question ve Survey arasındaki bire çok ilişki
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Survey)
                .WithMany(s => s.Questions)
                .HasForeignKey(s => s.SurveyId);
            #endregion

            #region Question ve Choice arasındaki bire çok ilişki
            modelBuilder.Entity<Choice>()
                .HasOne(c => c.Question)
                .WithMany(s => s.Choices)
                .HasForeignKey(c => c.QuestionId);
            #endregion

            #region Answer ve Choice arasındaki bire çok ilişki
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Choice)
                .WithMany(c => c.Answers)
                .HasForeignKey(a => a.ChoiceId);
            #endregion

            #region Participant ve Answer arasındaki bire çok ilişki
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Participant)
                .WithMany(c => c.Answers)
                .HasForeignKey(a => a.ParticipantId);
            #endregion

        }
    }
}