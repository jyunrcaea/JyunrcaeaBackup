﻿namespace Jyunrcaea
{
    internal sealed class SharedMemoryStream : Stream
    {
        private readonly object _lock;
        private readonly RefCounter _refCounter;
        private readonly MemoryStream _sourceMemoryStream;
        private bool _disposed;
        private long _position;

        public SharedMemoryStream(byte[] buffer) : this(new object(), new RefCounter(), new MemoryStream(buffer))
        {
        }

        private SharedMemoryStream(object @lock, RefCounter refCounter, MemoryStream sourceMemoryStream)
        {
            _lock = @lock;

            lock (_lock)
            {
                _refCounter = refCounter;
                _sourceMemoryStream = sourceMemoryStream;

                _refCounter.Count++;
            }
        }

        public override bool CanRead
        {
            get
            {
                lock (_lock)
                {
                    return !_disposed;
                }
            }
        }

        public override bool CanSeek
        {
            get
            {
                lock (_lock)
                {
                    return !_disposed;
                }
            }
        }

        public override bool CanWrite => false;

        public override long Length
        {
            get
            {
                lock (_lock)
                {
                    CheckIfDisposed();
                    return _sourceMemoryStream.Length;
                }
            }
        }

        public override long Position
        {
            get
            {
                lock (_lock)
                {
                    CheckIfDisposed();
                    return _position;
                }
            }
            set
            {
                lock (_lock)
                {
                    CheckIfDisposed();
                    _position = value;
                }
            }
        }

        // Creates another shallow copy of stream that uses the same underlying MemoryStream
        public SharedMemoryStream MakeShared()
        {
            lock (_lock)
            {
                CheckIfDisposed();
                return new SharedMemoryStream(_lock, _refCounter, _sourceMemoryStream);
            }
        }

        public override void Flush()
        {
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            lock (_lock)
            {
                CheckIfDisposed();

                _sourceMemoryStream.Position = Position;
                var seek = _sourceMemoryStream.Seek(offset, origin);
                Position = _sourceMemoryStream.Position;

                return seek;
            }
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException($"{nameof(SharedMemoryStream)} is read only stream.");
        }

        // Uses position that is unique for each copy of shared stream
        // to read underlying MemoryStream that is common for all shared copies
        public override int Read(byte[] buffer, int offset, int count)
        {
            lock (_lock)
            {
                CheckIfDisposed();

                _sourceMemoryStream.Position = Position;
                var read = _sourceMemoryStream.Read(buffer, offset, count);
                Position = _sourceMemoryStream.Position;

                return read;
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException($"{nameof(SharedMemoryStream)} is read only stream.");
        }

        // Reference counting to dispose underlying MemoryStream when all shared copies are disposed
        protected override void Dispose(bool disposing)
        {
            lock (_lock)
            {
                if (disposing)
                {
                    _disposed = true;
                    _refCounter.Count--;
                    if (_refCounter.Count == 0) _sourceMemoryStream?.Dispose();
                }
                base.Dispose(disposing);
            }
        }

        private void CheckIfDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(SharedMemoryStream));
        }

        private class RefCounter
        {
            public int Count;
        }
    }
}