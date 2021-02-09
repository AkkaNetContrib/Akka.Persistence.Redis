// -----------------------------------------------------------------------
// <copyright file="MessageFormats.cs" company="Akka.NET Project">
//      Copyright (C) 2013-2021 .NET Foundation <https://github.com/akkadotnet/akka.net>
// </copyright>
// -----------------------------------------------------------------------

#pragma warning disable 1591, 0612, 3021

#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;

namespace CustomSerialization.Protobuf.Msg
{
    /// <summary>Holder for reflection information generated from MessageFormats.proto</summary>
    public static partial class MessageFormatsReflection
    {
        #region Descriptor

        /// <summary>File descriptor for MessageFormats.proto</summary>
        public static pbr::FileDescriptor Descriptor
        {
            get { return descriptor; }
        }

        private static pbr::FileDescriptor descriptor;

        static MessageFormatsReflection()
        {
            byte[] descriptorData = global::System.Convert.FromBase64String(
                string.Concat(
                    "ChRNZXNzYWdlRm9ybWF0cy5wcm90bxIgQ3VzdG9tU2VyaWFsaXphdGlvbi5Q",
                    "cm90b2J1Zi5Nc2cimAEKEVBlcnNpc3RlbnRNZXNzYWdlEhUKDXBlcnNpc3Rl",
                    "bmNlSWQYASABKAkSEgoKc2VxdWVuY2VOchgCIAEoAxISCgp3cml0ZXJHdWlk",
                    "GAMgASgJEkQKB3BheWxvYWQYBCABKAsyMy5DdXN0b21TZXJpYWxpemF0aW9u",
                    "LlByb3RvYnVmLk1zZy5QZXJzaXN0ZW50UGF5bG9hZCJTChFQZXJzaXN0ZW50",
                    "UGF5bG9hZBIPCgdtZXNzYWdlGAEgASgMEhQKDHNlcmlhbGl6ZXJJZBgCIAEo",
                    "BRIXCg9tZXNzYWdlTWFuaWZlc3QYAyABKAwiFwoGU3RvcmVkEg0KBXZhbHVl",
                    "GAEgASgFYgZwcm90bzM="));
            descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
                new pbr::FileDescriptor[] { },
                new pbr::GeneratedClrTypeInfo(null,
                    new pbr::GeneratedClrTypeInfo[]
                    {
                        new pbr::GeneratedClrTypeInfo(
                            typeof(global::CustomSerialization.Protobuf.Msg.PersistentMessage),
                            global::CustomSerialization.Protobuf.Msg.PersistentMessage.Parser,
                            new[] {"PersistenceId", "SequenceNr", "WriterGuid", "Payload"}, null, null, null),
                        new pbr::GeneratedClrTypeInfo(
                            typeof(global::CustomSerialization.Protobuf.Msg.PersistentPayload),
                            global::CustomSerialization.Protobuf.Msg.PersistentPayload.Parser,
                            new[] {"Message", "SerializerId", "MessageManifest"}, null, null, null),
                        new pbr::GeneratedClrTypeInfo(typeof(global::CustomSerialization.Protobuf.Msg.Stored),
                            global::CustomSerialization.Protobuf.Msg.Stored.Parser, new[] {"Value"}, null, null,
                            null)
                    }));
        }

        #endregion
    }

    #region Messages

    public sealed partial class PersistentMessage : pb::IMessage<PersistentMessage>
    {
        private static readonly pb::MessageParser<PersistentMessage> _parser =
            new pb::MessageParser<PersistentMessage>(() => new PersistentMessage());

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pb::MessageParser<PersistentMessage> Parser
        {
            get { return _parser; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pbr::MessageDescriptor Descriptor
        {
            get { return global::CustomSerialization.Protobuf.Msg.MessageFormatsReflection.Descriptor.MessageTypes[0]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        pbr::MessageDescriptor pb::IMessage.Descriptor
        {
            get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public PersistentMessage()
        {
            OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public PersistentMessage(PersistentMessage other) : this()
        {
            persistenceId_ = other.persistenceId_;
            sequenceNr_ = other.sequenceNr_;
            writerGuid_ = other.writerGuid_;
            Payload = other.payload_ != null ? other.Payload.Clone() : null;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public PersistentMessage Clone()
        {
            return new PersistentMessage(this);
        }

        /// <summary>Field number for the "persistenceId" field.</summary>
        public const int PersistenceIdFieldNumber = 1;

        private string persistenceId_ = "";

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string PersistenceId
        {
            get { return persistenceId_; }
            set { persistenceId_ = pb::ProtoPreconditions.CheckNotNull(value, "value"); }
        }

        /// <summary>Field number for the "sequenceNr" field.</summary>
        public const int SequenceNrFieldNumber = 2;

        private long sequenceNr_;

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public long SequenceNr
        {
            get { return sequenceNr_; }
            set { sequenceNr_ = value; }
        }

        /// <summary>Field number for the "writerGuid" field.</summary>
        public const int WriterGuidFieldNumber = 3;

        private string writerGuid_ = "";

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string WriterGuid
        {
            get { return writerGuid_; }
            set { writerGuid_ = pb::ProtoPreconditions.CheckNotNull(value, "value"); }
        }

        /// <summary>Field number for the "payload" field.</summary>
        public const int PayloadFieldNumber = 4;

        private global::CustomSerialization.Protobuf.Msg.PersistentPayload payload_;

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public global::CustomSerialization.Protobuf.Msg.PersistentPayload Payload
        {
            get { return payload_; }
            set { payload_ = value; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override bool Equals(object other)
        {
            return Equals(other as PersistentMessage);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public bool Equals(PersistentMessage other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(other, this))
            {
                return true;
            }

            if (PersistenceId != other.PersistenceId) return false;
            if (SequenceNr != other.SequenceNr) return false;
            if (WriterGuid != other.WriterGuid) return false;
            if (!object.Equals(Payload, other.Payload)) return false;
            return true;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override int GetHashCode()
        {
            int hash = 1;
            if (PersistenceId.Length != 0) hash ^= PersistenceId.GetHashCode();
            if (SequenceNr != 0L) hash ^= SequenceNr.GetHashCode();
            if (WriterGuid.Length != 0) hash ^= WriterGuid.GetHashCode();
            if (payload_ != null) hash ^= Payload.GetHashCode();
            return hash;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override string ToString()
        {
            return pb::JsonFormatter.ToDiagnosticString(this);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void WriteTo(pb::CodedOutputStream output)
        {
            if (PersistenceId.Length != 0)
            {
                output.WriteRawTag(10);
                output.WriteString(PersistenceId);
            }

            if (SequenceNr != 0L)
            {
                output.WriteRawTag(16);
                output.WriteInt64(SequenceNr);
            }

            if (WriterGuid.Length != 0)
            {
                output.WriteRawTag(26);
                output.WriteString(WriterGuid);
            }

            if (payload_ != null)
            {
                output.WriteRawTag(34);
                output.WriteMessage(Payload);
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int CalculateSize()
        {
            int size = 0;
            if (PersistenceId.Length != 0)
            {
                size += 1 + pb::CodedOutputStream.ComputeStringSize(PersistenceId);
            }

            if (SequenceNr != 0L)
            {
                size += 1 + pb::CodedOutputStream.ComputeInt64Size(SequenceNr);
            }

            if (WriterGuid.Length != 0)
            {
                size += 1 + pb::CodedOutputStream.ComputeStringSize(WriterGuid);
            }

            if (payload_ != null)
            {
                size += 1 + pb::CodedOutputStream.ComputeMessageSize(Payload);
            }

            return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(PersistentMessage other)
        {
            if (other == null)
            {
                return;
            }

            if (other.PersistenceId.Length != 0)
            {
                PersistenceId = other.PersistenceId;
            }

            if (other.SequenceNr != 0L)
            {
                SequenceNr = other.SequenceNr;
            }

            if (other.WriterGuid.Length != 0)
            {
                WriterGuid = other.WriterGuid;
            }

            if (other.payload_ != null)
            {
                if (payload_ == null)
                {
                    payload_ = new global::CustomSerialization.Protobuf.Msg.PersistentPayload();
                }

                Payload.MergeFrom(other.Payload);
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(pb::CodedInputStream input)
        {
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        input.SkipLastField();
                        break;
                    case 10:
                    {
                        PersistenceId = input.ReadString();
                        break;
                    }
                    case 16:
                    {
                        SequenceNr = input.ReadInt64();
                        break;
                    }
                    case 26:
                    {
                        WriterGuid = input.ReadString();
                        break;
                    }
                    case 34:
                    {
                        if (payload_ == null)
                        {
                            payload_ = new global::CustomSerialization.Protobuf.Msg.PersistentPayload();
                        }

                        input.ReadMessage(payload_);
                        break;
                    }
                }
            }
        }
    }

    public sealed partial class PersistentPayload : pb::IMessage<PersistentPayload>
    {
        private static readonly pb::MessageParser<PersistentPayload> _parser =
            new pb::MessageParser<PersistentPayload>(() => new PersistentPayload());

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pb::MessageParser<PersistentPayload> Parser
        {
            get { return _parser; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pbr::MessageDescriptor Descriptor
        {
            get { return global::CustomSerialization.Protobuf.Msg.MessageFormatsReflection.Descriptor.MessageTypes[1]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        pbr::MessageDescriptor pb::IMessage.Descriptor
        {
            get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public PersistentPayload()
        {
            OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public PersistentPayload(PersistentPayload other) : this()
        {
            message_ = other.message_;
            serializerId_ = other.serializerId_;
            messageManifest_ = other.messageManifest_;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public PersistentPayload Clone()
        {
            return new PersistentPayload(this);
        }

        /// <summary>Field number for the "message" field.</summary>
        public const int MessageFieldNumber = 1;

        private pb::ByteString message_ = pb::ByteString.Empty;

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public pb::ByteString Message
        {
            get { return message_; }
            set { message_ = pb::ProtoPreconditions.CheckNotNull(value, "value"); }
        }

        /// <summary>Field number for the "serializerId" field.</summary>
        public const int SerializerIdFieldNumber = 2;

        private int serializerId_;

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int SerializerId
        {
            get { return serializerId_; }
            set { serializerId_ = value; }
        }

        /// <summary>Field number for the "messageManifest" field.</summary>
        public const int MessageManifestFieldNumber = 3;

        private pb::ByteString messageManifest_ = pb::ByteString.Empty;

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public pb::ByteString MessageManifest
        {
            get { return messageManifest_; }
            set { messageManifest_ = pb::ProtoPreconditions.CheckNotNull(value, "value"); }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override bool Equals(object other)
        {
            return Equals(other as PersistentPayload);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public bool Equals(PersistentPayload other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(other, this))
            {
                return true;
            }

            if (Message != other.Message) return false;
            if (SerializerId != other.SerializerId) return false;
            if (MessageManifest != other.MessageManifest) return false;
            return true;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override int GetHashCode()
        {
            int hash = 1;
            if (Message.Length != 0) hash ^= Message.GetHashCode();
            if (SerializerId != 0) hash ^= SerializerId.GetHashCode();
            if (MessageManifest.Length != 0) hash ^= MessageManifest.GetHashCode();
            return hash;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override string ToString()
        {
            return pb::JsonFormatter.ToDiagnosticString(this);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void WriteTo(pb::CodedOutputStream output)
        {
            if (Message.Length != 0)
            {
                output.WriteRawTag(10);
                output.WriteBytes(Message);
            }

            if (SerializerId != 0)
            {
                output.WriteRawTag(16);
                output.WriteInt32(SerializerId);
            }

            if (MessageManifest.Length != 0)
            {
                output.WriteRawTag(26);
                output.WriteBytes(MessageManifest);
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int CalculateSize()
        {
            int size = 0;
            if (Message.Length != 0)
            {
                size += 1 + pb::CodedOutputStream.ComputeBytesSize(Message);
            }

            if (SerializerId != 0)
            {
                size += 1 + pb::CodedOutputStream.ComputeInt32Size(SerializerId);
            }

            if (MessageManifest.Length != 0)
            {
                size += 1 + pb::CodedOutputStream.ComputeBytesSize(MessageManifest);
            }

            return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(PersistentPayload other)
        {
            if (other == null)
            {
                return;
            }

            if (other.Message.Length != 0)
            {
                Message = other.Message;
            }

            if (other.SerializerId != 0)
            {
                SerializerId = other.SerializerId;
            }

            if (other.MessageManifest.Length != 0)
            {
                MessageManifest = other.MessageManifest;
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(pb::CodedInputStream input)
        {
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        input.SkipLastField();
                        break;
                    case 10:
                    {
                        Message = input.ReadBytes();
                        break;
                    }
                    case 16:
                    {
                        SerializerId = input.ReadInt32();
                        break;
                    }
                    case 26:
                    {
                        MessageManifest = input.ReadBytes();
                        break;
                    }
                }
            }
        }
    }

    public sealed partial class Stored : pb::IMessage<Stored>
    {
        private static readonly pb::MessageParser<Stored> _parser = new pb::MessageParser<Stored>(() => new Stored());

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pb::MessageParser<Stored> Parser
        {
            get { return _parser; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pbr::MessageDescriptor Descriptor
        {
            get { return global::CustomSerialization.Protobuf.Msg.MessageFormatsReflection.Descriptor.MessageTypes[2]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        pbr::MessageDescriptor pb::IMessage.Descriptor
        {
            get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public Stored()
        {
            OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public Stored(Stored other) : this()
        {
            value_ = other.value_;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public Stored Clone()
        {
            return new Stored(this);
        }

        /// <summary>Field number for the "value" field.</summary>
        public const int ValueFieldNumber = 1;

        private int value_;

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int Value
        {
            get { return value_; }
            set { value_ = value; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override bool Equals(object other)
        {
            return Equals(other as Stored);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public bool Equals(Stored other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(other, this))
            {
                return true;
            }

            if (Value != other.Value) return false;
            return true;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override int GetHashCode()
        {
            int hash = 1;
            if (Value != 0) hash ^= Value.GetHashCode();
            return hash;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override string ToString()
        {
            return pb::JsonFormatter.ToDiagnosticString(this);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void WriteTo(pb::CodedOutputStream output)
        {
            if (Value != 0)
            {
                output.WriteRawTag(8);
                output.WriteInt32(Value);
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int CalculateSize()
        {
            int size = 0;
            if (Value != 0)
            {
                size += 1 + pb::CodedOutputStream.ComputeInt32Size(Value);
            }

            return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(Stored other)
        {
            if (other == null)
            {
                return;
            }

            if (other.Value != 0)
            {
                Value = other.Value;
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(pb::CodedInputStream input)
        {
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        input.SkipLastField();
                        break;
                    case 8:
                    {
                        Value = input.ReadInt32();
                        break;
                    }
                }
            }
        }
    }

    #endregion
}

#endregion Designer generated code