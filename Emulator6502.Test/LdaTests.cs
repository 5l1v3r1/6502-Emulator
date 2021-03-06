﻿using System;
using System.Diagnostics.Contracts;
using Emulator6502.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emulator6502.Test
{
    [TestClass]
    public class LdaTests
    {
        [TestMethod]
        public void Immediate()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.LdaImmediate);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 41);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(41, cpu.A);
        }

        [TestMethod]
        public void ZeroPage()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(0xF, 41);
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.LdaZeroPage);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0xF);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(41, cpu.A);
        }

        [TestMethod]
        public void ZeroPageX()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(0x1E, 0x10);
            cpu.Ram.WriteByte(0x1F, 0x5);
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.LdaZeroPageX);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0x1E);
            cpu.X = 0x1;

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0x5, cpu.A);
        }

        [TestMethod]
        public void Absolute()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(0x2310, 0x5);
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.LdaAbsolute);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0x10);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 2), 0x23);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0x5, cpu.A);
        }

        [TestMethod]
        public void AbsoluteX()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(0x2311, 0x5);
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.LdaAbsoluteX);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0x10);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 2), 0x23);
            cpu.X = 0x1;

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0x5, cpu.A);
        }

        [TestMethod]
        public void AbsoluteY()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(0x2311, 0x5);
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.LdaAbsoluteY);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0x10);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 2), 0x23);
            cpu.Y = 0x1;

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0x5, cpu.A);
        }

        [TestMethod]
        public void IndirectX()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(0x1C, 0x10);
            cpu.Ram.WriteByte(0x1D, 0x23);
            cpu.Ram.WriteByte(0x1E, 0x45);
            cpu.Ram.WriteByte(0x4523, 0x78);
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.LdaIndirectX);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0x1C);
            cpu.X = 0x1;

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0x78, cpu.A);
        }

        [TestMethod]
        public void IndirectY()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(0x1C, 0x10);
            cpu.Ram.WriteByte(0x1D, 0x23);
            cpu.Ram.WriteByte(0x2311, 0x78);
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.LdaIndirectY);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0x1C);
            cpu.Y = 0x1;

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0x78, cpu.A);
        }

        #region Status flag updates - all tested in immediate mode

        [TestMethod]
        public void ZeroFlagNotSet()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.LdaImmediate);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 1);

            // Act
            cpu.Execute();

            // Assert
            Assert.IsFalse(cpu.ZeroFlag);
        }

        [TestMethod]
        public void ZeroFlagSet()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.LdaImmediate);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0);

            // Act
            cpu.Execute();

            // Assert
            Assert.IsTrue(cpu.ZeroFlag);
        }

        [TestMethod]
        public void NegativeFlagNotSet()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.LdaImmediate);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 127);

            // Act
            cpu.Execute();

            // Assert
            Assert.IsFalse(cpu.NegativeFlag);
        }

        [TestMethod]
        public void NegativeFlagSet()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.LdaImmediate);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 255);

            // Act
            cpu.Execute();

            // Assert
            Assert.IsTrue(cpu.NegativeFlag);
        }

        #endregion
    }
}
