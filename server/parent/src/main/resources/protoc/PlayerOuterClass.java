// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: Player.proto

public final class PlayerOuterClass {
  private PlayerOuterClass() {}
  public static void registerAllExtensions(
      com.google.protobuf.ExtensionRegistryLite registry) {
  }

  public static void registerAllExtensions(
      com.google.protobuf.ExtensionRegistry registry) {
    registerAllExtensions(
        (com.google.protobuf.ExtensionRegistryLite) registry);
  }
  public interface PlayerOrBuilder extends
      // @@protoc_insertion_point(interface_extends:Player)
      com.google.protobuf.MessageOrBuilder {

    /**
     * <code>string accountIndex = 1;</code>
     * @return The accountIndex.
     */
    java.lang.String getAccountIndex();
    /**
     * <code>string accountIndex = 1;</code>
     * @return The bytes for accountIndex.
     */
    com.google.protobuf.ByteString
        getAccountIndexBytes();

    /**
     * <code>int64 time = 2;</code>
     * @return The time.
     */
    long getTime();
  }
  /**
   * Protobuf type {@code Player}
   */
  public static final class Player extends
      com.google.protobuf.GeneratedMessageV3 implements
      // @@protoc_insertion_point(message_implements:Player)
      PlayerOrBuilder {
  private static final long serialVersionUID = 0L;
    // Use Player.newBuilder() to construct.
    private Player(com.google.protobuf.GeneratedMessageV3.Builder<?> builder) {
      super(builder);
    }
    private Player() {
      accountIndex_ = "";
    }

    @java.lang.Override
    @SuppressWarnings({"unused"})
    protected java.lang.Object newInstance(
        UnusedPrivateParameter unused) {
      return new Player();
    }

    @java.lang.Override
    public final com.google.protobuf.UnknownFieldSet
    getUnknownFields() {
      return this.unknownFields;
    }
    private Player(
        com.google.protobuf.CodedInputStream input,
        com.google.protobuf.ExtensionRegistryLite extensionRegistry)
        throws com.google.protobuf.InvalidProtocolBufferException {
      this();
      if (extensionRegistry == null) {
        throw new java.lang.NullPointerException();
      }
      com.google.protobuf.UnknownFieldSet.Builder unknownFields =
          com.google.protobuf.UnknownFieldSet.newBuilder();
      try {
        boolean done = false;
        while (!done) {
          int tag = input.readTag();
          switch (tag) {
            case 0:
              done = true;
              break;
            case 10: {
              java.lang.String s = input.readStringRequireUtf8();

              accountIndex_ = s;
              break;
            }
            case 16: {

              time_ = input.readInt64();
              break;
            }
            default: {
              if (!parseUnknownField(
                  input, unknownFields, extensionRegistry, tag)) {
                done = true;
              }
              break;
            }
          }
        }
      } catch (com.google.protobuf.InvalidProtocolBufferException e) {
        throw e.setUnfinishedMessage(this);
      } catch (java.io.IOException e) {
        throw new com.google.protobuf.InvalidProtocolBufferException(
            e).setUnfinishedMessage(this);
      } finally {
        this.unknownFields = unknownFields.build();
        makeExtensionsImmutable();
      }
    }
    public static final com.google.protobuf.Descriptors.Descriptor
        getDescriptor() {
      return PlayerOuterClass.internal_static_Player_descriptor;
    }

    @java.lang.Override
    protected com.google.protobuf.GeneratedMessageV3.FieldAccessorTable
        internalGetFieldAccessorTable() {
      return PlayerOuterClass.internal_static_Player_fieldAccessorTable
          .ensureFieldAccessorsInitialized(
              PlayerOuterClass.Player.class, PlayerOuterClass.Player.Builder.class);
    }

    public static final int ACCOUNTINDEX_FIELD_NUMBER = 1;
    private volatile java.lang.Object accountIndex_;
    /**
     * <code>string accountIndex = 1;</code>
     * @return The accountIndex.
     */
    @java.lang.Override
    public java.lang.String getAccountIndex() {
      java.lang.Object ref = accountIndex_;
      if (ref instanceof java.lang.String) {
        return (java.lang.String) ref;
      } else {
        com.google.protobuf.ByteString bs = 
            (com.google.protobuf.ByteString) ref;
        java.lang.String s = bs.toStringUtf8();
        accountIndex_ = s;
        return s;
      }
    }
    /**
     * <code>string accountIndex = 1;</code>
     * @return The bytes for accountIndex.
     */
    @java.lang.Override
    public com.google.protobuf.ByteString
        getAccountIndexBytes() {
      java.lang.Object ref = accountIndex_;
      if (ref instanceof java.lang.String) {
        com.google.protobuf.ByteString b = 
            com.google.protobuf.ByteString.copyFromUtf8(
                (java.lang.String) ref);
        accountIndex_ = b;
        return b;
      } else {
        return (com.google.protobuf.ByteString) ref;
      }
    }

    public static final int TIME_FIELD_NUMBER = 2;
    private long time_;
    /**
     * <code>int64 time = 2;</code>
     * @return The time.
     */
    @java.lang.Override
    public long getTime() {
      return time_;
    }

    private byte memoizedIsInitialized = -1;
    @java.lang.Override
    public final boolean isInitialized() {
      byte isInitialized = memoizedIsInitialized;
      if (isInitialized == 1) return true;
      if (isInitialized == 0) return false;

      memoizedIsInitialized = 1;
      return true;
    }

    @java.lang.Override
    public void writeTo(com.google.protobuf.CodedOutputStream output)
                        throws java.io.IOException {
      if (!com.google.protobuf.GeneratedMessageV3.isStringEmpty(accountIndex_)) {
        com.google.protobuf.GeneratedMessageV3.writeString(output, 1, accountIndex_);
      }
      if (time_ != 0L) {
        output.writeInt64(2, time_);
      }
      unknownFields.writeTo(output);
    }

    @java.lang.Override
    public int getSerializedSize() {
      int size = memoizedSize;
      if (size != -1) return size;

      size = 0;
      if (!com.google.protobuf.GeneratedMessageV3.isStringEmpty(accountIndex_)) {
        size += com.google.protobuf.GeneratedMessageV3.computeStringSize(1, accountIndex_);
      }
      if (time_ != 0L) {
        size += com.google.protobuf.CodedOutputStream
          .computeInt64Size(2, time_);
      }
      size += unknownFields.getSerializedSize();
      memoizedSize = size;
      return size;
    }

    @java.lang.Override
    public boolean equals(final java.lang.Object obj) {
      if (obj == this) {
       return true;
      }
      if (!(obj instanceof PlayerOuterClass.Player)) {
        return super.equals(obj);
      }
      PlayerOuterClass.Player other = (PlayerOuterClass.Player) obj;

      if (!getAccountIndex()
          .equals(other.getAccountIndex())) return false;
      if (getTime()
          != other.getTime()) return false;
      if (!unknownFields.equals(other.unknownFields)) return false;
      return true;
    }

    @java.lang.Override
    public int hashCode() {
      if (memoizedHashCode != 0) {
        return memoizedHashCode;
      }
      int hash = 41;
      hash = (19 * hash) + getDescriptor().hashCode();
      hash = (37 * hash) + ACCOUNTINDEX_FIELD_NUMBER;
      hash = (53 * hash) + getAccountIndex().hashCode();
      hash = (37 * hash) + TIME_FIELD_NUMBER;
      hash = (53 * hash) + com.google.protobuf.Internal.hashLong(
          getTime());
      hash = (29 * hash) + unknownFields.hashCode();
      memoizedHashCode = hash;
      return hash;
    }

    public static PlayerOuterClass.Player parseFrom(
        java.nio.ByteBuffer data)
        throws com.google.protobuf.InvalidProtocolBufferException {
      return PARSER.parseFrom(data);
    }
    public static PlayerOuterClass.Player parseFrom(
        java.nio.ByteBuffer data,
        com.google.protobuf.ExtensionRegistryLite extensionRegistry)
        throws com.google.protobuf.InvalidProtocolBufferException {
      return PARSER.parseFrom(data, extensionRegistry);
    }
    public static PlayerOuterClass.Player parseFrom(
        com.google.protobuf.ByteString data)
        throws com.google.protobuf.InvalidProtocolBufferException {
      return PARSER.parseFrom(data);
    }
    public static PlayerOuterClass.Player parseFrom(
        com.google.protobuf.ByteString data,
        com.google.protobuf.ExtensionRegistryLite extensionRegistry)
        throws com.google.protobuf.InvalidProtocolBufferException {
      return PARSER.parseFrom(data, extensionRegistry);
    }
    public static PlayerOuterClass.Player parseFrom(byte[] data)
        throws com.google.protobuf.InvalidProtocolBufferException {
      return PARSER.parseFrom(data);
    }
    public static PlayerOuterClass.Player parseFrom(
        byte[] data,
        com.google.protobuf.ExtensionRegistryLite extensionRegistry)
        throws com.google.protobuf.InvalidProtocolBufferException {
      return PARSER.parseFrom(data, extensionRegistry);
    }
    public static PlayerOuterClass.Player parseFrom(java.io.InputStream input)
        throws java.io.IOException {
      return com.google.protobuf.GeneratedMessageV3
          .parseWithIOException(PARSER, input);
    }
    public static PlayerOuterClass.Player parseFrom(
        java.io.InputStream input,
        com.google.protobuf.ExtensionRegistryLite extensionRegistry)
        throws java.io.IOException {
      return com.google.protobuf.GeneratedMessageV3
          .parseWithIOException(PARSER, input, extensionRegistry);
    }
    public static PlayerOuterClass.Player parseDelimitedFrom(java.io.InputStream input)
        throws java.io.IOException {
      return com.google.protobuf.GeneratedMessageV3
          .parseDelimitedWithIOException(PARSER, input);
    }
    public static PlayerOuterClass.Player parseDelimitedFrom(
        java.io.InputStream input,
        com.google.protobuf.ExtensionRegistryLite extensionRegistry)
        throws java.io.IOException {
      return com.google.protobuf.GeneratedMessageV3
          .parseDelimitedWithIOException(PARSER, input, extensionRegistry);
    }
    public static PlayerOuterClass.Player parseFrom(
        com.google.protobuf.CodedInputStream input)
        throws java.io.IOException {
      return com.google.protobuf.GeneratedMessageV3
          .parseWithIOException(PARSER, input);
    }
    public static PlayerOuterClass.Player parseFrom(
        com.google.protobuf.CodedInputStream input,
        com.google.protobuf.ExtensionRegistryLite extensionRegistry)
        throws java.io.IOException {
      return com.google.protobuf.GeneratedMessageV3
          .parseWithIOException(PARSER, input, extensionRegistry);
    }

    @java.lang.Override
    public Builder newBuilderForType() { return newBuilder(); }
    public static Builder newBuilder() {
      return DEFAULT_INSTANCE.toBuilder();
    }
    public static Builder newBuilder(PlayerOuterClass.Player prototype) {
      return DEFAULT_INSTANCE.toBuilder().mergeFrom(prototype);
    }
    @java.lang.Override
    public Builder toBuilder() {
      return this == DEFAULT_INSTANCE
          ? new Builder() : new Builder().mergeFrom(this);
    }

    @java.lang.Override
    protected Builder newBuilderForType(
        com.google.protobuf.GeneratedMessageV3.BuilderParent parent) {
      Builder builder = new Builder(parent);
      return builder;
    }
    /**
     * Protobuf type {@code Player}
     */
    public static final class Builder extends
        com.google.protobuf.GeneratedMessageV3.Builder<Builder> implements
        // @@protoc_insertion_point(builder_implements:Player)
        PlayerOuterClass.PlayerOrBuilder {
      public static final com.google.protobuf.Descriptors.Descriptor
          getDescriptor() {
        return PlayerOuterClass.internal_static_Player_descriptor;
      }

      @java.lang.Override
      protected com.google.protobuf.GeneratedMessageV3.FieldAccessorTable
          internalGetFieldAccessorTable() {
        return PlayerOuterClass.internal_static_Player_fieldAccessorTable
            .ensureFieldAccessorsInitialized(
                PlayerOuterClass.Player.class, PlayerOuterClass.Player.Builder.class);
      }

      // Construct using PlayerOuterClass.Player.newBuilder()
      private Builder() {
        maybeForceBuilderInitialization();
      }

      private Builder(
          com.google.protobuf.GeneratedMessageV3.BuilderParent parent) {
        super(parent);
        maybeForceBuilderInitialization();
      }
      private void maybeForceBuilderInitialization() {
        if (com.google.protobuf.GeneratedMessageV3
                .alwaysUseFieldBuilders) {
        }
      }
      @java.lang.Override
      public Builder clear() {
        super.clear();
        accountIndex_ = "";

        time_ = 0L;

        return this;
      }

      @java.lang.Override
      public com.google.protobuf.Descriptors.Descriptor
          getDescriptorForType() {
        return PlayerOuterClass.internal_static_Player_descriptor;
      }

      @java.lang.Override
      public PlayerOuterClass.Player getDefaultInstanceForType() {
        return PlayerOuterClass.Player.getDefaultInstance();
      }

      @java.lang.Override
      public PlayerOuterClass.Player build() {
        PlayerOuterClass.Player result = buildPartial();
        if (!result.isInitialized()) {
          throw newUninitializedMessageException(result);
        }
        return result;
      }

      @java.lang.Override
      public PlayerOuterClass.Player buildPartial() {
        PlayerOuterClass.Player result = new PlayerOuterClass.Player(this);
        result.accountIndex_ = accountIndex_;
        result.time_ = time_;
        onBuilt();
        return result;
      }

      @java.lang.Override
      public Builder clone() {
        return super.clone();
      }
      @java.lang.Override
      public Builder setField(
          com.google.protobuf.Descriptors.FieldDescriptor field,
          java.lang.Object value) {
        return super.setField(field, value);
      }
      @java.lang.Override
      public Builder clearField(
          com.google.protobuf.Descriptors.FieldDescriptor field) {
        return super.clearField(field);
      }
      @java.lang.Override
      public Builder clearOneof(
          com.google.protobuf.Descriptors.OneofDescriptor oneof) {
        return super.clearOneof(oneof);
      }
      @java.lang.Override
      public Builder setRepeatedField(
          com.google.protobuf.Descriptors.FieldDescriptor field,
          int index, java.lang.Object value) {
        return super.setRepeatedField(field, index, value);
      }
      @java.lang.Override
      public Builder addRepeatedField(
          com.google.protobuf.Descriptors.FieldDescriptor field,
          java.lang.Object value) {
        return super.addRepeatedField(field, value);
      }
      @java.lang.Override
      public Builder mergeFrom(com.google.protobuf.Message other) {
        if (other instanceof PlayerOuterClass.Player) {
          return mergeFrom((PlayerOuterClass.Player)other);
        } else {
          super.mergeFrom(other);
          return this;
        }
      }

      public Builder mergeFrom(PlayerOuterClass.Player other) {
        if (other == PlayerOuterClass.Player.getDefaultInstance()) return this;
        if (!other.getAccountIndex().isEmpty()) {
          accountIndex_ = other.accountIndex_;
          onChanged();
        }
        if (other.getTime() != 0L) {
          setTime(other.getTime());
        }
        this.mergeUnknownFields(other.unknownFields);
        onChanged();
        return this;
      }

      @java.lang.Override
      public final boolean isInitialized() {
        return true;
      }

      @java.lang.Override
      public Builder mergeFrom(
          com.google.protobuf.CodedInputStream input,
          com.google.protobuf.ExtensionRegistryLite extensionRegistry)
          throws java.io.IOException {
        PlayerOuterClass.Player parsedMessage = null;
        try {
          parsedMessage = PARSER.parsePartialFrom(input, extensionRegistry);
        } catch (com.google.protobuf.InvalidProtocolBufferException e) {
          parsedMessage = (PlayerOuterClass.Player) e.getUnfinishedMessage();
          throw e.unwrapIOException();
        } finally {
          if (parsedMessage != null) {
            mergeFrom(parsedMessage);
          }
        }
        return this;
      }

      private java.lang.Object accountIndex_ = "";
      /**
       * <code>string accountIndex = 1;</code>
       * @return The accountIndex.
       */
      public java.lang.String getAccountIndex() {
        java.lang.Object ref = accountIndex_;
        if (!(ref instanceof java.lang.String)) {
          com.google.protobuf.ByteString bs =
              (com.google.protobuf.ByteString) ref;
          java.lang.String s = bs.toStringUtf8();
          accountIndex_ = s;
          return s;
        } else {
          return (java.lang.String) ref;
        }
      }
      /**
       * <code>string accountIndex = 1;</code>
       * @return The bytes for accountIndex.
       */
      public com.google.protobuf.ByteString
          getAccountIndexBytes() {
        java.lang.Object ref = accountIndex_;
        if (ref instanceof String) {
          com.google.protobuf.ByteString b = 
              com.google.protobuf.ByteString.copyFromUtf8(
                  (java.lang.String) ref);
          accountIndex_ = b;
          return b;
        } else {
          return (com.google.protobuf.ByteString) ref;
        }
      }
      /**
       * <code>string accountIndex = 1;</code>
       * @param value The accountIndex to set.
       * @return This builder for chaining.
       */
      public Builder setAccountIndex(
          java.lang.String value) {
        if (value == null) {
    throw new NullPointerException();
  }
  
        accountIndex_ = value;
        onChanged();
        return this;
      }
      /**
       * <code>string accountIndex = 1;</code>
       * @return This builder for chaining.
       */
      public Builder clearAccountIndex() {
        
        accountIndex_ = getDefaultInstance().getAccountIndex();
        onChanged();
        return this;
      }
      /**
       * <code>string accountIndex = 1;</code>
       * @param value The bytes for accountIndex to set.
       * @return This builder for chaining.
       */
      public Builder setAccountIndexBytes(
          com.google.protobuf.ByteString value) {
        if (value == null) {
    throw new NullPointerException();
  }
  checkByteStringIsUtf8(value);
        
        accountIndex_ = value;
        onChanged();
        return this;
      }

      private long time_ ;
      /**
       * <code>int64 time = 2;</code>
       * @return The time.
       */
      @java.lang.Override
      public long getTime() {
        return time_;
      }
      /**
       * <code>int64 time = 2;</code>
       * @param value The time to set.
       * @return This builder for chaining.
       */
      public Builder setTime(long value) {
        
        time_ = value;
        onChanged();
        return this;
      }
      /**
       * <code>int64 time = 2;</code>
       * @return This builder for chaining.
       */
      public Builder clearTime() {
        
        time_ = 0L;
        onChanged();
        return this;
      }
      @java.lang.Override
      public final Builder setUnknownFields(
          final com.google.protobuf.UnknownFieldSet unknownFields) {
        return super.setUnknownFields(unknownFields);
      }

      @java.lang.Override
      public final Builder mergeUnknownFields(
          final com.google.protobuf.UnknownFieldSet unknownFields) {
        return super.mergeUnknownFields(unknownFields);
      }


      // @@protoc_insertion_point(builder_scope:Player)
    }

    // @@protoc_insertion_point(class_scope:Player)
    private static final PlayerOuterClass.Player DEFAULT_INSTANCE;
    static {
      DEFAULT_INSTANCE = new PlayerOuterClass.Player();
    }

    public static PlayerOuterClass.Player getDefaultInstance() {
      return DEFAULT_INSTANCE;
    }

    private static final com.google.protobuf.Parser<Player>
        PARSER = new com.google.protobuf.AbstractParser<Player>() {
      @java.lang.Override
      public Player parsePartialFrom(
          com.google.protobuf.CodedInputStream input,
          com.google.protobuf.ExtensionRegistryLite extensionRegistry)
          throws com.google.protobuf.InvalidProtocolBufferException {
        return new Player(input, extensionRegistry);
      }
    };

    public static com.google.protobuf.Parser<Player> parser() {
      return PARSER;
    }

    @java.lang.Override
    public com.google.protobuf.Parser<Player> getParserForType() {
      return PARSER;
    }

    @java.lang.Override
    public PlayerOuterClass.Player getDefaultInstanceForType() {
      return DEFAULT_INSTANCE;
    }

  }

  private static final com.google.protobuf.Descriptors.Descriptor
    internal_static_Player_descriptor;
  private static final 
    com.google.protobuf.GeneratedMessageV3.FieldAccessorTable
      internal_static_Player_fieldAccessorTable;

  public static com.google.protobuf.Descriptors.FileDescriptor
      getDescriptor() {
    return descriptor;
  }
  private static  com.google.protobuf.Descriptors.FileDescriptor
      descriptor;
  static {
    java.lang.String[] descriptorData = {
      "\n\014Player.proto\",\n\006Player\022\024\n\014accountIndex" +
      "\030\001 \001(\t\022\014\n\004time\030\002 \001(\003b\006proto3"
    };
    descriptor = com.google.protobuf.Descriptors.FileDescriptor
      .internalBuildGeneratedFileFrom(descriptorData,
        new com.google.protobuf.Descriptors.FileDescriptor[] {
        });
    internal_static_Player_descriptor =
      getDescriptor().getMessageTypes().get(0);
    internal_static_Player_fieldAccessorTable = new
      com.google.protobuf.GeneratedMessageV3.FieldAccessorTable(
        internal_static_Player_descriptor,
        new java.lang.String[] { "AccountIndex", "Time", });
  }

  // @@protoc_insertion_point(outer_class_scope)
}
