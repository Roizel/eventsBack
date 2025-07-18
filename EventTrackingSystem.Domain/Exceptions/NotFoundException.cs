﻿namespace EventTrackingSystem.Domain.Exceptions;

public class NotFoundException(
  string name,
  object key
) : Exception($"Entity \"{name}\" ({key}) not found.")
{ }
